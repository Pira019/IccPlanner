using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateRoleRequest createRoleRequest)
        {
            try
            {
                await _roleService.CreateRole(createRoleRequest);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
