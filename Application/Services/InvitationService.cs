using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Invitation;
using Application.Responses.Invitation;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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

            if (invitation == null) { 

                return Result<InvitationResponse>.Fail(ValidationMessages.AJ_IdInvNonExist);
            }
            return Result<InvitationResponse>.Success(_mapper.Map<InvitationResponse>(invitation));
        }

        public async Task<Result<bool>> SendInvitationAnsyc(SendRequest request)
        {
            // Genrer le code d'invitation a 4 chiffres
            var code = new Random().Next(1000, 9999).ToString();
            // Date d'expiration de l'invitation (7 jours)
            var expirationDate = DateTime.UtcNow.AddMinutes(_appSettings.Parametres.DureeInvitationEnMin);

            bool isEmailUsed = await _invitationRepository.IsEmailUsedAsync(request.Email);

            if (isEmailUsed)
            {
                return Result<bool>.Fail(ValidationMessages.AJ_InvitExist);
            }

            var _invitation = await _invitationRepository.FindByEmail(request.Email);

            var invitation = new Invitation
            {
                Code = code,
                DateExpiration = expirationDate,
                DepartmentId = request.DepartmentID,
                Email = request.Email,
                FirstName = request.FirstName,
                AddBy = GetAuthenticatedUserId()
            }; 

            if (_invitation != null)
            {
                invitation.Id = _invitation.Id;
                invitation.DateSend = DateTime.UtcNow;
                invitation.DateExpiration = expirationDate;
                invitation.IndAct = true;
                invitation.UpdatedBy = GetAuthenticatedUserId();

                await _invitationRepository.Update_Async(invitation, _invitation);
            }
            else
            {
                await _invitationRepository.Insert(invitation);
            }

            // Récupérer le nom du département pour l'email
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentID);
            var departmentName = department?.Name ?? "Département";

            // Envoyer l'email d'invitation (non bloquant, géré dans SendEmailService)
            await _sendEmailService.SendInvitationEmail(
                request.Email,
                request.FirstName,
                departmentName,
                code,
                invitation.Id
            );
                
           return Result<bool>.Success(true);
        }

        /// <summary>
        ///     Utilisé pour obtenir l'ID de l'utilisateur authentifié à partir du contexte HTTP.
        /// </summary>
        /// <returns></returns>
        protected string GetAuthenticatedUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst("sub")?.Value ?? string.Empty;
        }         
        
    }
}
