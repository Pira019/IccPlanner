using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses;
using Application.Responses.Errors; 
using Domain.Abstractions;
using Domain.Entities; 
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
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType<ApiListReponse<Role>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var resul = await _roleService.GetAllRoles();
                return Ok(resul);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,RoleErrors.ERROR_CREATE_ROLE.Message);
                return BadRequest(ApiError.ApiIdentityResultResponseError());
            } 
        }

        /// <summary>
        /// Permet de créer un Role
        /// </summary>
        /// <param name="createRoleRequest"> Voir <see cref="CreateRoleRequest"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IActionResult"/> de l'opération </returns>
        [HttpPost]
       // [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest createRoleRequest)
        {
            try
            {
                var result = await _roleService.CreateRole(createRoleRequest);
                if (!result.Succeeded)
                {
                    var response = ApiError.ApiIdentityResultResponseError(result);
                    return BadRequest(response);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,RoleErrors.ERROR_CREATE_ROLE.Message);
                return BadRequest(ApiError.InternalServerError(RoleErrors.ERROR_CREATE_ROLE.Message));
            }
        }
    }
}
