using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses;
using Application.Responses.Errors;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        /// <summary>
        /// Permet de créer un Role
        /// </summary>
        /// <param name="createRoleRequest"> Voir <see cref="CreateRoleRequest"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IActionResult"/> de l'opération </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateRoleRequest createRoleRequest)
        {
            try
            {
                var result = await _roleService.CreateRole(createRoleRequest);

                if (!result.Succeeded)
                {
                    var response = ApiError.ApiErrorResponse(result);
                    return BadRequest(response);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return BadRequest(ApiError.ApiErrorResponse());
            }
        }
    }
}
