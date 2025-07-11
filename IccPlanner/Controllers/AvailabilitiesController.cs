using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Availability;
using Application.Responses;
using Application.Responses.Errors;
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
        private readonly ITabServicePrgRepository _tabServicePrgRepository;
        private readonly IAvailabilityRepository _availabilityRepository;

        private readonly IAvailabilityService _availabilityService;
        public AvailabilitiesController(ITabServicePrgRepository tabServicePrgRepository,
                   IAvailabilityRepository availabilityRepository,
                   IAvailabilityService availabilityService,
                   IAccountRepository accountRepository)
                   : base(accountRepository)
        {
            _tabServicePrgRepository = tabServicePrgRepository;
            _availabilityService = availabilityService;
            _availabilityRepository = availabilityRepository;
        }

        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseAddResponse>(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddAvailabilityRequest addAvailabilityRequest)
        {
            var servicePrgDepartmentId = await _tabServicePrgRepository.GetDepartmentIdByServicePrgId(addAvailabilityRequest.ServicePrgId);

            if (servicePrgDepartmentId == 0)
            {
                return BadRequest(ApiError.ErrorMessage(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.SERVICE_), null, null));
            }

            // Récupérer l'ID de l'utilisateur à partir des claims
            var userAuthId = Utiles.GetUserIdFromClaims(User);
            // Récupérer le membre authentifié
            var member = await GetMemberAuthIdAsync();

            if (member == Guid.Empty)
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.USER_NOT_FOUND, null, null));
            }

            var departMemberId = await _availabilityService.GetIdDepartmentMember(member, servicePrgDepartmentId);

            if (departMemberId == null)
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.SERVICE_NOT_AVAILABITY, null, null));
            }

            if (await _availabilityRepository.HasAlreadyChosenAvailability(addAvailabilityRequest.ServicePrgId, (int)departMemberId))
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.SERVICE_ALREADY_CHOSEN, null, null));
            }
            await _availabilityService.Add(addAvailabilityRequest, (int)departMemberId);
            return Created(string.Empty, null);
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
    }
}
