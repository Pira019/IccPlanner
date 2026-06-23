using System.Data;
using Application.Helper; 
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Department;
using Application.Responses;
using Application.Responses.Department;
using Application.Responses.Errors;
using Infrastructure.Security;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : PlannerBaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;

        public DepartmentsController(IDepartmentService departmentService, IAccountRepository accountRepository, IDepartmentMemberRepository departmentMemberRepository)
            : base(accountRepository)
        {
            _departmentService = departmentService;
            _departmentMemberRepository = departmentMemberRepository;
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
        public async Task<IActionResult> Get([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
            var userAuthId = Utiles.GetUserIdFromClaims(User)!;

            var departments = await _departmentService.GetAsync(userAuthId.ToString(), ClaimsConstants.CAN_MANANG_DEPART, pageNumber, pageSize);
            return Ok(departments);
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_MANG_DEPART)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddDepartmentResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(AddDepartmentRequest request)
        {
            var newDepartment = await _departmentService.AddDepartment(request);
            if(!newDepartment.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(newDepartment.Error, null, null));
            }
            return Created(string.Empty, newDepartment.Value);
        }

        /// <summary>
        ///     Permet de modifier un département existant.
        /// </summary>
        /// <param name="id">
        ///  Identifiant du département à modifier.
        /// </param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddDepartmentResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, AddDepartmentRequest request)
        {
            // Vérifier : claim CanManagDepart OU IndGest dans ce département
            var hasClaim = Utiles.HasPermission(User, ClaimsConstants.CAN_MANANG_DEPART, ClaimsConstants.PERMISSION);
            if (!hasClaim)
            {
                var memberId = await GetMemberAuthIdAsync();
                var hasRight = await _departmentMemberRepository.HasManagementRightAsync(memberId, id);
                if (!hasRight)
                    return BadRequest(ApiError.ErrorMessage(ValidationMessages.DEPARTMENT_UPDATE_NOT_AUTHORIZED, null, null));
            }

            var newDepartment = await _departmentService.UpdateDept(id, request);
            if (!newDepartment.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(newDepartment.Error, null, null));
            }
            return Ok();
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

       /// <summary>
       /// Deletes the department with the specified identifier using a soft-delete operation.
       /// </summary>
       /// <remarks>This operation performs a soft delete, marking the department as deleted without
       /// permanently removing it from the database. Requires appropriate authorization.</remarks>
       /// <param name="id">The unique identifier of the department to delete. Must correspond to an existing department.</param>
       /// <returns>An <see cref="IActionResult"/> indicating the result of the delete operation. Returns status code 200 (OK) if
       /// the deletion succeeds, or an appropriate error response if the request is unauthorized or forbidden.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyConstants.CAN_MANG_DEPART)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteSoftByIdAsync(id); 
            return Ok();
        }

        /// <summary>
        ///     Ajoute un programme aux département.
        /// </summary>
        [HttpPost("programs")]
        [Authorize(PolicyConstants.CAN_MANG_DEPART_DETAIL)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddDepartmentProgram([FromBody] AddDepartmentProgramRequest request)
        {            
            var userAuthId = Utiles.GetUserIdFromClaims(User)!;  
            var result = await _departmentService.AddDepartmentProgram(request, userAuthId.ToString());
            
            if (!result.IsSuccess)
            {
                if (result.CodeErreur == "RESTORE_OR_CREATE")
                {
                    return Conflict(ApiError.ErrorMessage(result.Error, null, null));
                }
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }  
            return Created();
        }

        /// <summary>
        ///     Restaure un DepartmentProgram soft-deleted.
        /// </summary>
        [HttpPost("programs/restore/{departmentProgramId}")]
        [Authorize(PolicyConstants.CAN_MANG_DEPART_DETAIL)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RestoreDepartmentProgram(int departmentProgramId)
        {
            await _departmentService.RestoreDepartmentProgramAsync(departmentProgramId);
            return Ok();
        }

        [HttpDelete("department-program")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDepartmentProgram([FromBody] DeleteDepartmentProgramRequest request)
        {
            var hasClaim = Utiles.HasPermission(User, ClaimsConstants.CAN_MANAGER_PRG, ClaimsConstants.PERMISSION)
                        || Utiles.HasPermission(User, ClaimsConstants.MANAGE_PRG_DETAIL, ClaimsConstants.PERMISSION)
                        || Utiles.HasPermission(User, ClaimsConstants.DEPART_MANAGER, ClaimsConstants.PERMISSION);
            if (!hasClaim)
            {
                // Fallback : vérifier IndGest sur le département concerné
                var memberId = await GetMemberAuthIdAsync();
                var departmentId = await _departmentService.GetDepartmentIdByDepartmentProgramIdAsync(request.DepartmentProgramIds);
                if (departmentId == null || !await _departmentMemberRepository.HasManagementRightAsync(memberId, departmentId.Value))
                    return BadRequest(ApiError.ErrorMessage(ValidationMessages.CANT_DELETE_DEPARTMENT_PROGRAM, null, null));
            }

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

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<DeptResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        { 
            var dept = await _departmentService.GetByIdAsync(id);
            return Ok(dept);
        }

        /// <summary>
        ///     Récupère les détails complets d'un département (infos, membres, postes, programmes).
        /// </summary>
        /// <param name="id">Id du département</param>
        /// <returns>Détails complets du département</returns>
        [HttpGet("{id}/details")]
        [Authorize]
        [ProducesResponseType<DepartmentDetailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetails(int id)
        {
            var detail = await _departmentService.GetDetailAsync(id);
            if (detail == null)
            {
                return NotFound(ApiError.ErrorMessage(ValidationMessages.DEPARTMENT_NOT_EXIST, null, null));
            }
            return Ok(detail);
        }

        [HttpGet("{id}/postes")]
        [Authorize]
        [ProducesResponseType<List<PosteResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPostes(int id)
        {
            var postes = await _departmentService.GetPostesByDepartmentAsync(id);
            return Ok(postes);
        }

        [HttpPost("{id}/postes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignPostes(int id, AssignPostesRequest request)
        {
            var hasClaim = Utiles.HasPermission(User, ClaimsConstants.DEPART_MANAGER, ClaimsConstants.PERMISSION);
            if (!hasClaim)
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.DEPARTMENT_UPDATE_NOT_AUTHORIZED, null, null));

            var result = await _departmentService.AssignPostesAsync(id, request.PosteIds);
            if (!result.IsSuccess)
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            return Ok();
        }

        /// <summary>
        ///     Désaffecte un poste d'un département.
        /// </summary>
        [HttpDelete("{id}/postes/{posteId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemovePoste(int id, int posteId)
        {
            var hasClaim = Utiles.HasPermission(User, ClaimsConstants.DEPART_MANAGER, ClaimsConstants.PERMISSION);
            if (!hasClaim)
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.DEPARTMENT_UPDATE_NOT_AUTHORIZED, null, null));

            await _departmentService.RemovePosteFromDepartmentAsync(id, posteId);
            return Ok();
        }

        /// <summary>
        ///     Affecte des postes à un membre du département.
        /// </summary>
        [HttpPost("{id}/members/{departmentMemberId}/postes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignPostesToMember(int id, int departmentMemberId, [FromBody] AssignPostesRequest request)
        {
            var hasClaim = Utiles.HasPermission(User, ClaimsConstants.CAN_MANANG_DEPART, ClaimsConstants.PERMISSION);
            if (!hasClaim)
            {
                var memberId = await GetMemberAuthIdAsync();
                var hasRight = await _departmentMemberRepository.HasManagementRightAsync(memberId, id);
                if (!hasRight)
                    return BadRequest(ApiError.ErrorMessage(ValidationMessages.DEPARTMENT_UPDATE_NOT_AUTHORIZED, null, null));
            }

            var result = await _departmentService.AssignPostesToMemberAsync(departmentMemberId, request.PosteIds);
            if (!result.IsSuccess)
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            return Ok();
        }


    }
}
