using Application.Helper;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses;
using Application.Responses.Department;
using Application.Responses.Errors.Department;
using Domain.Abstractions;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMinistryService _ministryService;
        public DepartmentsController(IDepartmentService departmentService, IMinistryService ministryService)
        {
            _departmentService = departmentService;
            _ministryService = ministryService;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_DEPARTMENT)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddDepartmentResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(AddDepartmentRequest request)
        {
            var newDepartment = await _departmentService.AddDepartment(request);
            return Created(string.Empty, newDepartment);
        }

        [HttpPost("responsable")]
        [Authorize(Policy = PolicyConstants.CAN_ATTRIBUT_DEPARTMENT_CHEF)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddResponsable([FromBody] AddDepartmentRespoRequest request)
        {
            await _departmentService.AddDepartmentResponsable(request);
            return Ok();
        }

        [HttpPost("programs")]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_DEPARTMENT_PROGRAM)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateDepartmentProgram([FromBody] AddDepartmentProgramRequest request)
        {
            var userAuthId = Utiles.GetUserIdFromClaims(User);
            await _departmentService.AddDepartmentsProgram(request, userAuthId);
            return Ok();
        }

        [HttpDelete("department-program")]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_DEPARTMENT_PROGRAM)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDepartmentProgram([FromBody] DeleteDepartmentProgramRequest request)
        {
            await _departmentService.DeleteDepartmentProgramByIdsAsync(request);
            return NoContent();
        }

        [HttpPost("import-members")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportMembers([FromForm] AddDepartmentMemberImportFileRequest request)
        {
            var userAuthId = Utiles.GetUserIdFromClaims(User);
            var result = await _departmentService.ImportMembersAsync(request, userAuthId);
            return Ok(result);
        }


    }
}
