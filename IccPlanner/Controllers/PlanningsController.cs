using Application.Helper;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.Requests.Planning;
using Application.Responses.Errors;
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
    }
}
