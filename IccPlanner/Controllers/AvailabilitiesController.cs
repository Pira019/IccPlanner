using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Availability;
using Application.Responses;
using Application.Responses.Availability;
using Application.Responses.Errors;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IccPlanner.Controllers
{
    /// <summary>
    ///     Permet des gérer les disponibilités.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitiesController : PlannerBaseController
    { 
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IAvailabilityService _availabilityService;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;

        public AvailabilitiesController(
                   IAvailabilityRepository availabilityRepository,
                   IAvailabilityService availabilityService,
                   IAccountRepository accountRepository,
                   IDepartmentMemberRepository departmentMemberRepository)
                   : base(accountRepository)
        { 
            _availabilityService = availabilityService;
            _availabilityRepository = availabilityRepository;
            _departmentMemberRepository = departmentMemberRepository;
        }

        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{idDepart}")]
        public async Task<IActionResult> Post(int idDepart, [FromBody] AddAvailabilityRequest addAvailabilityRequest)
        {
            var rsq = await _availabilityService.Add(addAvailabilityRequest, idDepart);

            if (!rsq.IsSuccess) 
            {
                return BadRequest(ApiError.ErrorMessage(rsq.Error, null, null));
            }

            return Created();
        }

        [HttpDelete("{idTabServicePrg}")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseAddResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int idTabServicePrg)
        {
            var avability = await _availabilityRepository.GetIdByAsync(idTabServicePrg, await GetMemberAuthIdAsync());

            if (avability == null)
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.AVAILAIBILITY_NOT_FOUND, null, null));
            }

            if (avability?.DatePrg <= DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.CANT_DELETE_AVAIBILITY, null, null));
            }
            await _availabilityRepository.DeleteAsync((int)avability?.Id!);
            return Ok();
        }

        [HttpGet("me/{departmentId}/{month}/{year}")]
        [Authorize]
        [ProducesResponseType<List<UserAvailabilityResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyAvailabilities(int departmentId, int month, int year)
        {
            var memberId = await GetMemberAuthIdAsync();
            var result = await _availabilityService.GetUserAvailabilitiesAsync(memberId, month, year, departmentId);
            return Ok(result.Value);
        }

        /// <summary>
        ///     Récupère les membres disponibles par département et date, groupés par service.
        ///     L'utilisateur doit être membre du département avec un poste IndGest = true
        ///     OU avoir le claim depart:manager contenant l'id du département.
        /// </summary>
        [HttpGet("{departmentId}/{date}")]
        [Authorize]
        [ProducesResponseType<List<AvailableMembersByDateResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAvailableMembersByDate(int departmentId, DateOnly date)
        {
            // Vérifier le claim depart:manager (format: depart:manager:1,2,4)
            var hasClaim = Utiles.HasDepartmentPermission(User, ClaimsConstants.DEPART_MANAGER, departmentId, ClaimsConstants.PERMISSION);

            if (!hasClaim)
            {
                // Vérifier si le membre a un poste avec IndGest = true dans ce département
                var memberId = await GetMemberAuthIdAsync();
                var hasManagementRight = await _departmentMemberRepository.HasManagementRightAsync(memberId, departmentId);

                if (!hasManagementRight)
                    return Forbid();
            }

            var result = await _availabilityService.GetAvailableMembersByDateAsync(departmentId, date);
            return Ok(result.Value);
        }
    }
}
