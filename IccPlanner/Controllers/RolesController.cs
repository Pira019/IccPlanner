using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses;
using Application.Responses.Errors;
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
        [ProducesResponseType<GetRolesResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var roles = await _roleService.GetAll();
            var result = new GetRolesResponse { Items = roles };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<CreateRoleResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRole(CreateRoleRequest createRoleRequest)
        {
            var newRole = await _roleService.CreateRole(createRoleRequest);

            if (!newRole.Succeeded)
            {
                return BadRequest(ApiError.ApiIdentityResultResponseError(newRole));
            } 
            //get role 
            var result = await _roleService.GetRoleByName(createRoleRequest.Name);
            return Created(string.Empty, result);
        }
    }
}
