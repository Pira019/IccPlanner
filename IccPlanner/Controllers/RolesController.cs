using Application.Interfaces.Services;
using Application.Responses;
using Application.Responses.Role;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(Policy = PolicyConstants.CAN_READ_ROLE)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<GetRolesResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var roles = await _roleService.GetAll();
            var result = new GetRolesResponse{ Items = roles };

            return Ok(result);
        }

    }
}
