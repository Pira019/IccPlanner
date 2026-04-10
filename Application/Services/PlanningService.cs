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
        private readonly IPlanningPeriodRepository _planningPeriodRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;

        public PlanningService(
            IPlanningRepository planningRepository,
            IPlanningPeriodRepository planningPeriodRepository,
            IAvailabilityRepository availabilityRepository,
            IDepartmentMemberRepository departmentMemberRepository)
        {
            _planningRepository = planningRepository;
            _planningPeriodRepository = planningPeriodRepository;
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
                return Result<AssignMemberResponse>.Fail(ValidationMessages.PLANNING_PAST_DATE);
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
                var conflictDetails = string.Join(" et ", conflicts.Select(c =>
                    $"« {c.ServiceName} » ({c.StartTime}-{c.EndTime}) - {c.DepartmentName}"));

                return Result<AssignMemberResponse>.Success(new AssignMemberResponse
                {
                    IsWarning = true,
                    Message = $"{ValidationMessages.PLANNING_OVERLAP} {conflictDetails}."
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

            // Marquer le planning comme ayant des modifications non publiées
            if (period.IndPublished)
            {
                period.IndPublished = false;
                await _planningPeriodRepository.UpdateAsync(period);
            }

            // Étape 6 — Le membre apparaît comme planifié
            return Result<AssignMemberResponse>.Success(new AssignMemberResponse
            {
                PlanningId = created.Id
            });
        }

        public async Task<List<MonthlyPlanningResponse>> GetMonthlyPlanningAsync(int month, int year, int departmentId)
        {
            return await _planningRepository.GetMonthlyPlanningAsync(month, year, departmentId);
        }

        public async Task<PlanningPeriodStatusResponse?> GetPeriodStatusAsync(int departmentId, int month, int year)
        {
            var period = await _planningPeriodRepository.GetByDepartmentMonthYearAsync(departmentId, month, year);
            if (period == null)
            {
                return null;
            }

            return new PlanningPeriodStatusResponse
            {
                IndPublished = period.IndPublished,
                IndArchived = period.IndArchived,
                PublishedAt = period.PublishedAt
            };
        }

        public async Task<List<MyPlanningResponse>> GetMyPlanningAsync(Guid memberId, int month, int year, int? departmentId)
        {
            return await _planningPeriodRepository.GetMyPlanningAsync(memberId, month, year, departmentId);
        }

        public async Task<List<TeamPlanningResponse>> GetTeamPlanningAsync(int departmentId, int month, int year)
        {
            return await _planningPeriodRepository.GetTeamPlanningAsync(departmentId, month, year);
        }

        /// <summary>
        ///     Retirer un membre du planning.
        ///     Vérifications : droit, existence, archivé, date passée.
        /// </summary>
        public async Task<Result<bool>> UnassignMemberAsync(int planningId, Guid actionById)
        {
            // Étape 1 — Le planning existe
            var planning = await _planningRepository.GetByIdWithPeriodAsync(planningId);
            if (planning == null)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_NOT_FOUND);
            }

            // Étape 2 — L'utilisateur a le droit d'assigner sur ce département
            var hasRight = await _departmentMemberRepository.HasPlanningRightAsync(
                actionById, planning.PlanningPeriod.DepartmentId);
            if (!hasRight)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_NOT_AUTHORIZED);
            }

            // Étape 3 — Le planning n'est pas archivé
            if (planning.PlanningPeriod.IndArchived)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_ARCHIVED);
            }

            // Étape 4 — La date n'est pas passée
            if (planning.ProgramDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_PAST_DATE);
            }

            // Étape 5 — Supprimer le planning
            await _planningRepository.DeleteAsync(planningId);

            // Marquer le planning comme ayant des modifications non publiées
            if (planning.PlanningPeriod.IndPublished)
            {
                planning.PlanningPeriod.IndPublished = false;
                await _planningPeriodRepository.UpdateAsync(planning.PlanningPeriod);
            }

            return Result<bool>.Success(true);
        }

        /// <summary>
        ///     Modifier une assignation existante (poste, formation, observation, commentaire).
        ///     Vérifications : existence, droit, archivé, date passée.
        /// </summary>
        public async Task<Result<bool>> UpdatePlanningAsync(int planningId, UpdatePlanningRequest request, Guid actionById)
        {
            // Étape 1 — Le planning existe
            var planning = await _planningRepository.GetByIdWithPeriodAsync(planningId);
            if (planning == null)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_NOT_FOUND);
            }

            // Étape 2 — L'utilisateur a le droit
            var hasRight = await _departmentMemberRepository.HasPlanningRightAsync(
                actionById, planning.PlanningPeriod.DepartmentId);
            if (!hasRight)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_NOT_AUTHORIZED);
            }

            // Étape 3 — Le planning n'est pas archivé
            if (planning.PlanningPeriod.IndArchived)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_ARCHIVED);
            }

            // Étape 4 — La date n'est pas passée
            if (planning.ProgramDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_PAST_DATE);
            }

            // Étape 5 — Mettre à jour les champs
            planning.PosteId = request.PosteId;
            planning.IndTraining = request.IndTraining;
            planning.IndObservation = request.IndObservation;
            planning.Comment = request.Comment;
            planning.UpdatedById = actionById;

            await _planningRepository.UpdateAsync(planning);

            // Marquer le planning comme ayant des modifications non publiées
            if (planning.PlanningPeriod.IndPublished)
            {
                planning.PlanningPeriod.IndPublished = false;
                await _planningPeriodRepository.UpdateAsync(planning.PlanningPeriod);
            }

            return Result<bool>.Success(true);
        }

        /// <summary>
        ///     Publier le planning : copie les Plannings dans PublishedPlannings (snapshot).
        ///     Les membres voient uniquement PublishedPlannings.
        /// </summary>
        public async Task<Result<bool>> PublishPlanningAsync(int departmentId, int month, int year, Guid actionById)
        {
            // Étape 1 — Vérifier les droits
            var hasRight = await _departmentMemberRepository.HasPlanningRightAsync(actionById, departmentId);
            if (!hasRight)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_NOT_AUTHORIZED);
            }

            // Étape 2 — Récupérer le PlanningPeriod
            var period = await _planningPeriodRepository.GetByDepartmentMonthYearAsync(departmentId, month, year);
            if (period == null)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_NOT_FOUND);
            }

            // Étape 3 — Vérifier que le planning n'est pas archivé
            if (period.IndArchived)
            {
                return Result<bool>.Fail(ValidationMessages.PLANNING_ARCHIVED);
            }

            // Étape 4 — Supprimer l'ancien snapshot
            await _planningPeriodRepository.DeletePublishedPlanningsAsync(period.Id);

            // Étape 5 — Créer le nouveau snapshot depuis les Plannings
            var snapshot = await _planningRepository.GetPlanningsForPublishAsync(period.Id);
            await _planningPeriodRepository.AddPublishedPlanningsAsync(snapshot);

            // Étape 6 — Mettre à jour le PlanningPeriod
            period.IndPublished = true;
            period.PublishedAt = DateTime.UtcNow;
            period.PublishedById = actionById;
            await _planningPeriodRepository.UpdateAsync(period);

            return Result<bool>.Success(true);
        }
    }
}
