using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Planning;
using Application.Responses.Planning;
using Domain.Entities;
using Shared.Ressources;

namespace Application.Services
{
    public class PlanningService : IPlanningService
    {
        private readonly IPlanningRepository _planningRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;

        public PlanningService(
            IPlanningRepository planningRepository,
            IAvailabilityRepository availabilityRepository,
            IDepartmentMemberRepository departmentMemberRepository)
        {
            _planningRepository = planningRepository;
            _availabilityRepository = availabilityRepository;
            _departmentMemberRepository = departmentMemberRepository;
        }

        public async Task<Result<AssignMemberResponse>> AssignMemberAsync(
            AssignMemberRequest request, Guid actionById, int departmentId)
        {
            // Extension 3a — L'utilisateur n'a pas le droit d'assigner (IndPlanning = false)
            var hasRight = await _departmentMemberRepository.HasPlanningRightAsync(actionById, departmentId);
            if (!hasRight)
            {
                return Result<AssignMemberResponse>.Fail(ValidationMessages.PLANNING_NOT_AUTHORIZED);
            }

            // Extension 3b — La disponibilité n'existe pas ou a été supprimée
            var availability = await _availabilityRepository.GetByIdAsync(request.AvailabilityId);
            if (availability == null)
            {
                return Result<AssignMemberResponse>.Fail(ValidationMessages.PLANNING_AVAILABILITY_NOT_FOUND);
            }

            // Extension 3d — Le membre est déjà assigné à ce service
            var alreadyAssigned = await _planningRepository.ExistsByAvailabilityIdAsync(request.AvailabilityId);
            if (alreadyAssigned)
            {
                return Result<AssignMemberResponse>.Fail(ValidationMessages.PLANNING_MEMBER_ALREADY_ASSIGNED);
            }

            // Étape 4 — Récupérer les détails de la disponibilité
            var detail = await _planningRepository.GetByAvailabilityDetailAsync(request.AvailabilityId);
            if (detail == null)
            {
                return Result<AssignMemberResponse>.Fail(ValidationMessages.PLANNING_AVAILABILITY_NOT_FOUND);
            }

            // Extension 3e — La date du programme est passée
            if (detail.ProgramDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return Result<AssignMemberResponse>.Fail(ValidationMessages.CANT_ADD_PAST_AVAILABILITY);
            }

            // Étape 4 — Vérifier qu'un PlanningPeriod existe, sinon le créer
            var period = await _planningRepository.GetOrCreatePeriodAsync(
                detail.DepartmentId, detail.Month, detail.Year);

            // Extension 3c — Le planning du mois est archivé
            if (period.IndArchived)
            {
                return Result<AssignMemberResponse>.Fail(ValidationMessages.PLANNING_ARCHIVED);
            }

            // Extension 3g — Chevauchement horaire (tous départements)
            var conflicts = await _planningRepository.GetOverlappingAssignmentsAsync(
                detail.MemberId, detail.ProgramDate, detail.StartTime, detail.EndTime);

            if (conflicts.Any() && !request.ForceAssign)
            {
                // Construire le message avec les détails des conflits
                var conflictDetails = string.Join("; ", conflicts.Select(c =>
                    $"« {c.ServiceName} » ({c.StartTime}-{c.EndTime}) - {c.DepartmentName}"));

                return Result<AssignMemberResponse>.Success(new AssignMemberResponse
                {
                    IsWarning = true,
                    Message = $"{ValidationMessages.PLANNING_OVERLAP} : {conflictDetails}"
                });
            }

            // Étape 5 — Créer l'enregistrement Planning
            var planning = new Planning
            {
                AvailabilityId = request.AvailabilityId,
                PlanningPeriodId = period.Id,
                MemberName = detail.MemberName,
                ProgramDate = detail.ProgramDate,
                ProgrammedById = actionById,
                PosteId = request.PosteId,
                IndTraining = request.IndTraining,
                IndObservation = request.IndObservation,
                Comment = request.Comment
            };

            var created = await _planningRepository.Insert(planning);

            // Étape 6 — Le membre apparaît comme planifié
            return Result<AssignMemberResponse>.Success(new AssignMemberResponse
            {
                PlanningId = created.Id
            });
        }
    }
}
