using Application.Dtos;
using Application.Dtos.Program;
using Application.Helper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses;
using Application.Responses.Errors;
using Application.Responses.Program;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources; 

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : PlannerBaseController
    {
        private readonly IProgramService _programService; 

        public ProgramsController(IAccountRepository accountRepository,IProgramService programService) : base(accountRepository)
        {
            _programService = programService;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_MANAG_PROGRAM)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddProgramResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] AddProgramRequest request)
        {
            var userAuthId = Utiles.GetUserIdFromClaims(User)!;
            var res = await _programService.Add(request, userAuthId.ToString(),ClaimsConstants.CAN_MANAGER_PRG);

            if (!res.IsSuccess) 
            {
                return BadRequest(ApiError.ErrorMessage(res.Error, null, null));
            }             
            return Created(string.Empty, res.Value);
        }

        [HttpGet]
        [Authorize(Policy = PolicyConstants.CAN_MANAG_PROGRAM)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType<PaginatedDto<ProgramDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProgram(int page=1,int pageSize = 25)
        {
            var result = await _programService.GetPaginatedProgram(page,pageSize);
            return Ok(result);
        }

        [HttpPost("filter")]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<GetProgramFilterResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProgramFilterAsync([FromBody] GetProgramFilterRequest filter)
        {
            return Ok(/* await _programRepository.GetProgramFilterAsync(filter) */);
        }

        [HttpPut("{id}")]
        [Authorize()]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] AddProgramRequest request)
        {
            var userAuthId = Utiles.GetUserIdFromClaims(User)!;
            var res = await _programService.Update(id, request, userAuthId.ToString(), ClaimsConstants.CAN_MANAGER_PRG);

            if (!res.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(res.Error, null, null));
            }
            return Ok();
        }
    }
}
