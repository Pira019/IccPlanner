using System.Security.Claims;
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
using Microsoft.Extensions.Localization;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMinistryService _ministryService;
        private readonly ILogger<DepartmentsController> _logger;
        public DepartmentsController(IDepartmentService departmentService, IMinistryService ministryService, ILogger<DepartmentsController> logger)
        {
            _departmentService = departmentService;
            _ministryService = ministryService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_DEPARTMENT)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddDepartmentResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(AddDepartmentRequest request)
        {
            var isMinistryExist = await _ministryService.IsMinistryExistsById(request.MinistryId);

            if (!isMinistryExist)
            {
                return BadRequest(DepartmentResponseError.ErrorMessage(MinistryError.NAME_NOT_FOUND));
            }

            var isDepartmentNameExist = await _departmentService.IsNameExists(request.Name);

            if (isDepartmentNameExist)
            {
                return BadRequest(DepartmentResponseError.ErrorMessage(DepartmentError.NAME_EXISTS));
            }
            var result = await _departmentService.AddDepartment(request);
            return Created(string.Empty, result);
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
        [Consumes("multipart/form-data")]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult ImportMembers([FromForm] AddDepartmentMemberImportFileRequest request)
        { 
            return NoContent();
        }


    }
}
