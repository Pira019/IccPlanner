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
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramsController(IProgramService programService)
        {
            this._programService = programService;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_PROGRAM)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddProgramResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProgram([FromBody] AddProgramRequest request)
        {
            bool isProgramexist = await _programService.IsNameExists(request.Name);

            if (isProgramexist) 
            { 
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.EXIST,ValidationMessages.PROGRAM_NAME,request.Name));
            }            
            var result = await _programService.Add(request);
            return Created(string.Empty, result);
        }
    }
}
