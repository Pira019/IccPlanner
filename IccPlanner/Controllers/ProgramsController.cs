﻿using Application.Dtos;
using Application.Dtos.Program;
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
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IProgramRepository _programRepository;

        public ProgramsController(IProgramService programService, IProgramRepository programRepository)
        {
            this._programService = programService;
            this._programRepository = programRepository;
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

        [HttpGet]
        [Authorize(Policy = PolicyConstants.CAN_CREATE_PROGRAM)]
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
            return Ok( await _programRepository.GetProgramFilterAsync(filter) );
        }
    }
}
