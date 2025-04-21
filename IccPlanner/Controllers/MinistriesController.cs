using Application.Interfaces.Services;
using Application.Requests.Ministry;
using Application.Responses; 
using Application.Responses.Ministry;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType<AddMinistryResponse> (StatusCodes.Status201Created)]
        public async Task<IActionResult>Add([FromBody] AddMinistryRequest request) 
        { 
            var result = await _ministryService.AddMinistry(request);
            return Created(string.Empty, result);
        }
    }
}
