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
        private readonly IMinistryRepository _ministryRepository;
        public MinistriesController(IMinistryService ministryService,IMinistryRepository ministryRepository)
        {
            _ministryService = ministryService;
            _ministryRepository = ministryRepository;
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
        public async Task<IActionResult> Put([FromBody] EditMinistryRequest request, [FromServices] IMinistryRepository ministryRepository)
        { 
            var ministryAn = await ministryRepository.GetByIdAsync((int)request.Id!);

            if (ministryAn == null)
            {
                return NotFound(string.Empty);
            }

            // 1a.	Le nom de ministère modifier existe 
            if (!string.Equals(ministryAn?.Name, request.Name, StringComparison.OrdinalIgnoreCase) && await ministryRepository.IsNameExists(request.Name))
            {

                return BadRequest(ApiError.ErrorMessage(ValidationMessages.MO_MinistryNameExist, null, null));
            }

            var newMinistry = new Ministry
            {
                Id = ministryAn!.Id,
                Name = request.Name,
                Description = request.Description
            };

            await ministryRepository.UpdateAsync(newMinistry, ministryAn!);

            return Ok();
        }

        /// <summary>
        ///     Supprimer un ministère
        /// </summary>
        /// <param name="id">
        ///     Indentifiant du ministère à supprimer
        /// </param>
        /// <param name="isConfirm">
        ///     Flag de confirmation de suppression
        /// </param>
        /// <param name="ministryRepository">
        ///     Référence du répository de ministère
        /// </param>
        /// <returns>
        ///     Resultat de l'opération de suppression
        /// </returns>
        [HttpDelete]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_MINISTRY)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponseWarning>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id, bool isConfirm = false)
        {
            var ministryAn = await _ministryRepository.IsExist(id);

            if (!ministryAn)
            {
                return NotFound(string.Empty);
            }

            var hasDepartments = await _ministryRepository.HasDepartmentAsync(id);
            if (!isConfirm && hasDepartments)
            {
                return BadRequest(ApiError.ErrorMessageWarning(ValidationMessages.DE_Ministry, null, null));
            }

            await _ministryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
