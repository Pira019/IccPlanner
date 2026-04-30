using System.Globalization;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Poste;
using Application.Responses.Department;
using Domain.Entities;
using Shared.Ressources;

namespace Application.Services
{
    public class PosteService : IPosteService
    {
        private readonly IPostRepository _postRepository;

        public PosteService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        private static string CapitalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return name;
            return char.ToUpper(name[0]) + name[1..].ToLower();
        }

        public async Task<Result<PosteResponse>> AddAsync(AddPosteRequest request)
        {
            var exists = await _postRepository.IsNameExistsAsync(request.Name);
            if (exists)
                return Result<PosteResponse>.Fail(string.Format(ValidationMessages.EXIST_Pro_Val, "Poste", request.Name));

            var poste = new Poste
            {
                Name = CapitalizeName(request.Name),
                Description = request.Description,
                ShortName = request.ShortName,
                IndGest = request.IndGest
            };

            var created = await _postRepository.InsertAsync(poste);
            return Result<PosteResponse>.Success(new PosteResponse
            {
                Id = created.Id,
                Name = created.Name,
                ShortName = created.ShortName,
                Description = created.Description,
                IndGest = created.IndGest,
                IndSystem = created.IndSystem
            });
        }

        public async Task<Result<PosteResponse>> UpdateAsync(int id, AddPosteRequest request)
        {
            var existing = await _postRepository.GetByIdAsync(id);
            if (existing == null)
                return Result<PosteResponse>.Fail(string.Format(ValidationMessages.NOT_EXIST, "Poste"));

            var nameExists = await _postRepository.IsNameExistsAsync(request.Name);
            if (existing.Name.ToLower() != request.Name.ToLower() && nameExists)
                return Result<PosteResponse>.Fail(string.Format(ValidationMessages.EXIST_Pro_Val, "Poste", request.Name));

            existing.Name = CapitalizeName(request.Name);
            existing.Description = request.Description;
            existing.ShortName = request.ShortName;
            existing.IndGest = request.IndGest;

            await _postRepository.UpdateAsync(existing);
            return Result<PosteResponse>.Success(new PosteResponse
            {
                Id = existing.Id,
                Name = existing.Name,
                ShortName = existing.ShortName,
                Description = existing.Description,
                IndGest = existing.IndGest,
                IndSystem = existing.IndSystem
            });
        }

        public async Task<List<PosteResponse>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<Result<PosteResponse>> GetByIdAsync(int id)
        {
            var poste = await _postRepository.GetByIdAsync(id);
            if (poste == null)
                return Result<PosteResponse>.Fail(string.Format(ValidationMessages.NOT_EXIST, "Poste"));

            return Result<PosteResponse>.Success(new PosteResponse
            {
                Id = poste.Id,
                Name = poste.Name,
                ShortName = poste.ShortName,
                Description = poste.Description,
                IndGest = poste.IndGest,
                IndSystem = poste.IndSystem
            });
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var poste = await _postRepository.GetByIdAsync(id);
            if (poste == null)
            {
                return Result<bool>.Fail(string.Format(ValidationMessages.NOT_EXIST, "Poste"));
            }

            if (poste.IndSystem)
            {
                return Result<bool>.Fail("Ce poste est un poste systeme et ne peut pas etre supprime.");
            }

            await _postRepository.DeleteAsync(id);
            return Result<bool>.Success(true);
        }

        /// <summary>
        ///     Liste des postes par défaut du système.
        /// </summary>
        private static readonly List<(string ShortName, string Name, string Description, int Order)> DefaultPostes =
        [
            ("RESPO", "Responsable", "Il est le référent du département.", 1),
            ("PLAN", "Planificateur", "Planifie et organise les tâches du département pendant les activités.", 2),
            ("COORDO", "Coordinateur", "Assure la coordination des équipes et des tâches.", 3),
            ("GESTI", "Gestionnaire", "Gère les ressources et le suivi administratif.", 4),
            ("RADACT", "Rédacteur", "Veille aux messages envoyés dans le groupe.", 5),
        ];

        /// <inheritdoc />
        public async Task SeedDefaultPostesAsync()
        {
            var existingShortNames = await _postRepository.GetAllShortNamesAsync();

            var toInsert = DefaultPostes
                .Where(p => !existingShortNames.Contains(p.ShortName))
                .Select(p => new Poste
                {
                    ShortName = p.ShortName,
                    Name = p.Name,
                    Description = p.Description,
                    DisplayOrder = p.Order,
                    IndSystem = true
                })
                .ToList();

            if (toInsert.Count > 0)
            {
                await _postRepository.InsertRangeAsync(toInsert);
            }
        }
    }
}
