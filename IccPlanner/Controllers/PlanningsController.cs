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
        /// </summary>
        [HttpPost("{departmentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Assign(int departmentId, [FromBody] AssignMemberRequest request)
        {
            var memberId = await GetMemberAuthIdAsync();
            var result = await _planningService.AssignMemberAsync(request, memberId, departmentId);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }

            if (result.Value.IsWarning)
            {
                return BadRequest(ApiError.ErrorMessageWarning(result.Value.Message!, null, null));
            }

            return Created(string.Empty, new { planningId = result.Value.PlanningId });
        }

        /// <summary>
        ///     Récapitulatif mensuel du planning.
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
        ///     Statut du PlanningPeriod.
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
        ///     Mon planning — assignations publiées du membre connecté.
        /// </summary>
        [HttpGet("my-planning/{month:int}/{year:int}")]
        [Authorize]
        [ProducesResponseType<List<MyPlanningResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyPlanning(int month, int year, [FromQuery] int? departmentId = null)
        {
            var memberId = await GetMemberAuthIdAsync();
            var result = await _planningService.GetMyPlanningAsync(memberId, month, year, departmentId);
            return Ok(result);
        }

        /// <summary>
        ///     Retirer un membre du planning.
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
        ///     Modifier une assignation existante.
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
        ///     Publier le planning d'un département.
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

        /// <summary>
        ///     Générer le PDF de l'horaire mensuel.
        /// </summary>
        [HttpGet("{month:int}/{year:int}/pdf")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSchedulePdf(int month, int year, [FromQuery] int departmentId)
        {
            var pdf = await _planningService.GenerateSchedulePdfAsync(departmentId, month, year);
            if (pdf == null)
            {
                return NotFound();
            }
            return File(pdf, "application/pdf", $"Planification-{month:D2}-{year}.pdf");
        }

        /// <summary>
        ///     Générer le PDF du planning journalier.
        /// </summary>
        [HttpGet("daily-pdf/{date}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDailyPdf(DateOnly date, [FromQuery] int departmentId)
        {
            var pdf = await _planningService.GenerateDailyPdfAsync(departmentId, date);
            if (pdf == null)
            {
                return NotFound();
            }
            return File(pdf, "application/pdf", $"Planification-{date:yyyy-MM-dd}.pdf");
        }
    }
}
