using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses;
using Application.Responses.Errors;
using Domain.Entities;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
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
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return BadRequest(ApiError.ApiErrorResponse());
            }

        }

        /// <summary>
        /// Permet de créer un Role
        /// </summary>
        /// <param name="createRoleRequest"> Voir <see cref="CreateRoleRequest"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IActionResult"/> de l'opération </returns>
        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
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
