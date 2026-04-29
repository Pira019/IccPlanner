using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Invitation;
using Application.Responses.Invitation;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Shared.Ressources;

namespace Application.Services
{
    public class InvitationService : BaseService<Invitation>, IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppSettings _appSettings;
        private readonly ISendEmailService _sendEmailService;
        private readonly IDepartmentRepository _departmentRepository;

        public InvitationService(IBaseRepository<Invitation> baseRepository, IMapper mapper,
            IInvitationRepository invitationRepository, IHttpContextAccessor httpContextAccessor,
            IAppSettings appSettings, ISendEmailService sendEmailService,
            IDepartmentRepository departmentRepository) : base(baseRepository, mapper)
        {
            _invitationRepository = invitationRepository;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
            _sendEmailService = sendEmailService;
            _departmentRepository = departmentRepository;
        }

        public async Task<Result<InvitationResponse>> FindValidInviation(int id)
        {
            var invitation = await _invitationRepository.FindValidInv(id);
            if (invitation == null)
            {
                return Result<InvitationResponse>.Fail(ValidationMessages.AJ_IdInvNonExist);
            }
            return Result<InvitationResponse>.Success(_mapper.Map<InvitationResponse>(invitation));
        }

        public async Task<Result<bool>> SendInvitationAnsyc(SendRequest request)
        {
            bool isEmailUsed = await _invitationRepository.IsEmailUsedAsync(request.Email);
            if (isEmailUsed)
            {
                return Result<bool>.Fail(ValidationMessages.AJ_InvitExist);
            }

            var existing = await _invitationRepository.FindByEmail(request.Email);
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentID);
            var departmentName = department?.Name ?? "Département";

            var invitation = await ProcessInvitationAsync(
                request.Email, request.FirstName, request.DepartmentID,
                existing, departmentName);

            return Result<bool>.Success(true);
        }

        /// <inheritdoc />
        public async Task<Result<BulkInviteResponse>> BulkInviteAsync(BulkInviteRequest request)
        {
            var response = new BulkInviteResponse();

            using var stream = new MemoryStream();
            request.File.CopyTo(stream);
            stream.Position = 0;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                return Result<BulkInviteResponse>.Fail(ValidationMessages.FILE_EMPTY);
            }

            // Trouver les index des colonnes par nom
            var columnIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                var header = worksheet.Cells[1, col].Text.Trim();
                if (!string.IsNullOrEmpty(header))
                {
                    columnIndexes[header] = col;
                }
            }

            if (!columnIndexes.ContainsKey("Prenom") || !columnIndexes.ContainsKey("Email"))
            {
                return Result<BulkInviteResponse>.Fail(ValidationMessages.FILE_INVALID_COLUMNS);
            }

            // 1. Lire et filtrer les lignes
            var validRows = new List<(int Row, string FirstName, string Email)>();
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var firstName = worksheet.Cells[row, columnIndexes["Prenom"]].Text.Trim();
                var email = worksheet.Cells[row, columnIndexes["Email"]].Text.Trim();

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(email))
                {
                    response.Skipped++;
                    response.Errors.Add($"Ligne {row}: Prénom ou Email vide.");
                    continue;
                }
                validRows.Add((row, firstName, email));
            }

            if (validRows.Count == 0)
            {
                return Result<BulkInviteResponse>.Success(response);
            }

            // 2. Une seule requête : charger toutes les invitations existantes
            var allEmails = validRows.Select(r => r.Email).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var existingInvitations = await _invitationRepository.FindByEmailsAsync(allEmails);
            var existingByEmail = existingInvitations
                .GroupBy(i => i.Email.ToLower())
                .ToDictionary(g => g.Key, g => g.OrderByDescending(i => i.Id).First());

            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
            var departmentName = department?.Name ?? "Département";
            var newInvitations = new List<Invitation>();

            // 3. Traiter chaque ligne
            foreach (var r in validRows)
            {
                var emailLower = r.Email.ToLower();
                existingByEmail.TryGetValue(emailLower, out var existing);

                if (existing != null && existing.IndUsed)
                {
                    response.Skipped++;
                    response.Errors.Add($"Ligne {r.Row}: {r.Email} — déjà utilisé.");
                    continue;
                }

                var invitation = PrepareInvitation(r.Email, r.FirstName, request.DepartmentId, existing);

                if (existing == null)
                {
                    newInvitations.Add(invitation);
                }

                response.Sent++;
            }

            // 4. Bulk insert des nouvelles (1 requête)
            if (newInvitations.Count > 0)
            {
                await _invitationRepository.InsertAllAsync(newInvitations);
            }
            else if (existingInvitations.Any())
            {
                // Si pas de nouvelles mais des updates, sauvegarder les modifications trackées
                await _invitationRepository.UpdateAsync(existingInvitations.First());
            }

            // 6. Envoyer les emails (non bloquant)
            foreach (var r in validRows)
            {
                var emailLower = r.Email.ToLower();
                existingByEmail.TryGetValue(emailLower, out var existing);
                if (existing != null && existing.IndUsed)
                {
                    continue;
                }

                var inv = existing ?? newInvitations.FirstOrDefault(i => i.Email.ToLower() == emailLower);
                if (inv != null)
                {
                    await _sendEmailService.SendInvitationEmail(r.Email, r.FirstName, departmentName, inv.Code, inv.Id);
                }
            }

            return Result<BulkInviteResponse>.Success(response);
        }

        /// <summary>
        ///     Logique commune : prépare une invitation (nouvelle ou mise à jour).
        ///     Si existing != null, met à jour l'existante. Sinon, crée une nouvelle.
        /// </summary>
        private Invitation PrepareInvitation(string email, string firstName, int departmentId, Invitation? existing)
        {
            var code = new Random().Next(1000, 9999).ToString();
            var expirationDate = DateTime.UtcNow.AddMinutes(_appSettings.Parametres.DureeInvitationEnMin);
            var userId = GetAuthenticatedUserId();

            if (existing != null)
            {
                existing.Code = code;
                existing.DateSend = DateTime.UtcNow;
                existing.DateExpiration = expirationDate;
                existing.IndAct = true;
                existing.UpdatedBy = userId;
                existing.FirstName = firstName;
                existing.DepartmentId = departmentId;
                return existing;
            }

            return new Invitation
            {
                Code = code,
                DateExpiration = expirationDate,
                DepartmentId = departmentId,
                Email = email,
                FirstName = firstName,
                AddBy = userId
            };
        }

        /// <summary>
        ///     Logique commune pour traiter une invitation unitaire (crée ou met à jour + envoie l'email).
        /// </summary>
        private async Task<Invitation> ProcessInvitationAsync(string email, string firstName, int departmentId, Invitation? existing, string departmentName)
        {
            var invitation = PrepareInvitation(email, firstName, departmentId, existing);

            if (existing != null)
            {
                await _invitationRepository.Update_Async(invitation, existing);
            }
            else
            {
                await _invitationRepository.Insert(invitation);
            }

            await _sendEmailService.SendInvitationEmail(email, firstName, departmentName, invitation.Code, invitation.Id);
            return invitation;
        }

        /// <summary>
        ///     Obtient l'ID de l'utilisateur authentifié à partir du contexte HTTP.
        /// </summary>
        protected string GetAuthenticatedUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst("sub")?.Value ?? string.Empty;
        }
    }
}
