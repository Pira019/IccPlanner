using Application.Helper.Validators.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Ministry;
using Application.Responses;
using Application.Responses.Errors;
using Application.Responses.Ministry;
using Domain.Entities;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinistriesController : ControllerBase
    {
        private readonly IMinistryService _ministryService;

        public MinistriesController(IMinistryService ministryService)
        {
            _ministryService = ministryService;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_MINISTRY)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddMinistryResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] AddMinistryRequest request)
        {
            var result = await _ministryService.AddMinistry(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<GetMinistriesResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ministryService.GetAll());
        }

        [HttpPut]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_MINISTRY)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] AddMinistryRequest request, [FromServices] IMinistryRepository ministryRepository)
        {
            var validator = new MjMinistryRequestValidation();

            // Valider la requête
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(ApiError.ErrorMessage(validationResult.Errors.First().ErrorMessage, null, null));
            }

            var ministryAn = await ministryRepository.GetById(request.Id);

            if (ministryAn == null)
            {
                return NotFound( string.Empty);
            }

            // 1a.	Le nom de ministère modifier existe 
            if (!string.Equals(ministryAn?.Name, request.Name, StringComparison.OrdinalIgnoreCase) && await ministryRepository.IsNameExists(request.Name))
            {

                return BadRequest(ApiError.ErrorMessage(ValidationMessages.MO_MinistryNameExist, null, null));
            }

            var newMinistry = new Ministry
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };

            await ministryRepository.UpdateAsync(newMinistry, ministryAn!);

            return Ok();
        }
    }
}
