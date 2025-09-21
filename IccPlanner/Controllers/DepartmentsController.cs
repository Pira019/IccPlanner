using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses;
using Application.Responses.Department;
using Application.Responses.Errors; 
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService; 
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService; 
        }

        /// <summary>
        ///     GetAsync all departments.
        /// </summary>
        /// <returns>
        ///     Retourne une liste de <see cref="DepartmentResponse"/>.
        /// </returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GetDepartResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var userAuthId = Utiles.GetUserIdFromClaims(User)!;
            var canViewInfoClaims = new List<string?>
            {
                ClaimsConstants.CAN_MANANG_DEPART,
            };

            var departments = await _departmentService.GetAsync(userAuthId.ToString(), canViewInfoClaims);
            return Ok(departments);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDepartmentProgram([FromBody] AddDepartmentProgramRequest request, IProgramRepository programRepository, IDepartmentProgramRepository departmentProgramRepository)
        {
            //Check si les départements sont vides
            if (!await _departmentService.IsValidDepartmentIds(request.DepartmentIds)) 
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.DEPARTMENT_INVALID_IDS,null, null));
            }
            //Check si le programme existe
            if (!await programRepository.IsExist(request.ProgramId))
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.INVALID_ENTRY, ValidationMessages.PROGRAM_NAME, null));
            }

            //Check si le programme existe
            var isDepartmentProgramExisting = await departmentProgramRepository.GetFirstExistingDepartmentProgramAsync(request.DepartmentIds, request.ProgramId, request.TypePrg);            
            
            if (isDepartmentProgramExisting != null)
            {
                return BadRequest(ApiError.ErrorMessage(String.Format(ValidationMessages.DEPARTMENT_PROGRAM_EXIST,isDepartmentProgramExisting.Department.Name, isDepartmentProgramExisting.Program.Name,request.TypePrg), null, null));
            }

            var userAuthId = Utiles.GetUserIdFromClaims(User)!;
            await _departmentService.AddDepartmentsProgram(request, userAuthId);
            return Created(string.Empty,string.Empty);
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
