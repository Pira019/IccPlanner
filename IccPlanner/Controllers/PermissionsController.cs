using Application.Responses.Account;
using Application.Responses; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.Responses.Permission;
using Infrastructure.Security.Constants;

namespace IccPlanner.Controllers
{
    /// <summary>
    ///     Cette classe permet de gérer les permissions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private IPermissionRepository _permission; 

        public PermissionsController(IPermissionRepository permission)
        {
            _permission = permission;
        }

        [HttpGet]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_ROLE)]
        [ProducesResponseType<GetAllPermissionResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll(IMapper mapper)
        {   
            var permissions = await _permission.GetAllAsync();
            return Ok(mapper.Map<List<GetAllPermissionResponse>> (permissions) );
        }
    }
}
