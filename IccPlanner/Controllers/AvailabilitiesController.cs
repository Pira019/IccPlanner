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
        private readonly IAvailabilityRepository _availabilityRepository;

        private readonly IAvailabilityService _availabilityService;
        public AvailabilitiesController(
                   IAvailabilityRepository availabilityRepository,
                   IAvailabilityService availabilityService,
                   IAccountRepository accountRepository)
                   : base(accountRepository)
        { 
            _availabilityService = availabilityService;
            _availabilityRepository = availabilityRepository;
        }

        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<int>(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddAvailabilityRequest addAvailabilityRequest)
        {
            var rsq = await _availabilityService.Add(addAvailabilityRequest);

            if (!rsq.IsSuccess) 
            {
                return BadRequest(ApiError.ErrorMessage(rsq.Error, null, null));
            }

            return Created(string.Empty, rsq.Value);
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
