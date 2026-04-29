using Application.Interfaces.Repositories;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRepository _accountRepository;

        public RolesController(IRoleService roleService, IRoleRepository roleRepository, IAccountRepository accountRepository)
        {
            _roleService = roleService;
            _roleRepository = roleRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [Authorize(Policy = PolicyConstants.CAN_READ_ROLE)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<GetRolesResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok( await _roleRepository.GetAllRoles());
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<CreateRoleResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest createRoleRequest)
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

        /// <summary>
        ///     Récupère tous les utilisateurs avec leurs rôles.
        /// </summary>
        [HttpGet("users")]
        [Authorize(Policy = PolicyConstants.CAN_READ_ROLE)]
        [ProducesResponseType<List<UserWithRolesResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _accountRepository.GetAllUsersAsync();
            var result = new List<UserWithRolesResponse>();

            foreach (var user in users)
            {
                var roles = await _accountRepository.GetUserRoles(user);
                result.Add(new UserWithRolesResponse
                {
                    UserId = user.Id,
                    DisplayName = user.Member.Name + " " + (user.Member.LastName ?? ""),
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return Ok(result);
        }

        /// <summary>
        ///     Assigne un rôle à un utilisateur et ajoute les claims correspondantes.
        /// </summary>
        [HttpPost("assign")]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var result = await _roleService.AssignRoleAsync(request.UserId, request.RoleName);
            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }
            return Ok();
        }

        /// <summary>
        ///     Retire un rôle à un utilisateur et supprime les claims correspondantes.
        /// </summary>
        [HttpPost("unassign")]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnassignRole([FromBody] AssignRoleRequest request)
        {
            var result = await _roleService.UnassignRoleAsync(request.UserId, request.RoleName);
            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }
            return Ok();
        }

    }
}
