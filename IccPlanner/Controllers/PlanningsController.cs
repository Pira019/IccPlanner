using Application.Helper;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.Requests.Planning;
using Application.Responses.Errors;
using Application.Responses.Planning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Responses;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningsController : PlannerBaseController
    {
        private readonly IPlanningService _planningService;

        public PlanningsController(
            IPlanningService planningService,
            IAccountRepository accountRepository)
            : base(accountRepository)
        {
            _planningService = planningService;
        }

        /// <summary>
        ///     Assigner un membre au planning.
        ///     Voir documentation : Scénario principal - Assigner un membre
        /// </summary>
        [HttpPost("{departmentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Assign(int departmentId, [FromBody] AssignMemberRequest request)
        {
            var memberId = await GetMemberAuthIdAsync();

            // Étapes 2 à 6 — Vérifications + création du planning
            var result = await _planningService.AssignMemberAsync(request, memberId, departmentId);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }

            // Extension 3g — Warning de chevauchement (format ApiErrorResponseWarning)
            if (result.Value.IsWarning)
            {
                return BadRequest(ApiError.ErrorMessageWarning(result.Value.Message!, null, null));
            }

            // Étape 6 — Créé avec succès
            return Created(string.Empty, new { planningId = result.Value.PlanningId });
        }

        /// <summary>
        ///     Récapitulatif mensuel du planning, groupé par programme.
        ///     Le département est obligatoire.
        /// </summary>
        [HttpGet("{month:int}/{year:int}")]
        [Authorize]
        [ProducesResponseType<List<MonthlyPlanningResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthlyPlanning(int month, int year, [FromQuery] int departmentId)
        {
            var result = await _planningService.GetMonthlyPlanningAsync(month, year, departmentId);
            return Ok(result);
        }

        /// <summary>
        ///     Statut du PlanningPeriod (publié, archivé, date de publication).
        /// </summary>
        [HttpGet("{month:int}/{year:int}/status")]
        [Authorize]
        [ProducesResponseType<PlanningPeriodStatusResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeriodStatus(int month, int year, [FromQuery] int departmentId)
        {
            var result = await _planningService.GetPeriodStatusAsync(departmentId, month, year);
            return Ok(result);
        }

        /// <summary>
        ///     Retirer un membre du planning.
        ///     Voir documentation : Scénario principal - Retirer un membre
        /// </summary>
        [HttpDelete("{planningId:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Unassign(int planningId)
        {
            var memberId = await GetMemberAuthIdAsync();

            var result = await _planningService.UnassignMemberAsync(planningId, memberId);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }

            return NoContent();
        }

        /// <summary>
        ///     Modifier une assignation existante (poste, formation, observation, commentaire).
        /// </summary>
        [HttpPut("{planningId:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int planningId, [FromBody] UpdatePlanningRequest request)
        {
            var memberId = await GetMemberAuthIdAsync();

            var result = await _planningService.UpdatePlanningAsync(planningId, request, memberId);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }

            return NoContent();
        }

        /// <summary>
        ///     Publier le planning d'un département pour un mois/année.
        ///     Crée un snapshot dans PublishedPlannings.
        /// </summary>
        [HttpPost("{departmentId:int}/publish")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Publish(int departmentId, [FromQuery] int month, [FromQuery] int year)
        {
            var memberId = await GetMemberAuthIdAsync();

            var result = await _planningService.PublishPlanningAsync(departmentId, month, year, memberId);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }

            return Ok();
        }
    }
}
