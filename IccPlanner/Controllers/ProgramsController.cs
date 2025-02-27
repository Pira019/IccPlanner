using Application.Interfaces.Services;
using Application.Requests.Program;
using Application.Responses; 
using Application.Responses.Program;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramService programService;

        public ProgramsController(IProgramService programService)
        {
            this.programService = programService;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_PROGRAM)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<AddProgramResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProgram([FromBody] AddProgramRequest request)
        {
            var result = await programService.Add(request);

            return Created(string.Empty, result);
        }
    }
}
