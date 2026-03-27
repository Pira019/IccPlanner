using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Responses;
using Application.Responses.Errors;
using Application.Responses.Invitation;
using Application.Responses.Member;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : PlannerBaseController
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService, IAccountRepository accountRepository)
            : base(accountRepository)
        {
            _memberService = memberService;
        }

        [HttpGet("{departmentId}")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<MembersResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int departmentId)
        {
            var response = await _memberService.GetByDepartmentIdAsync(departmentId);

            if (!response.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(response.Error, null, null));
            }
            return Ok(response.Value);
        }

        [HttpGet("belongs/{departmentId}")]
        [Authorize]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        public async Task<IActionResult> BelongsToDepartment(int departmentId)
        {
            var memberId = await GetMemberAuthIdAsync();
            var belongs = await _memberService.IsMemberInDepartmentAsync(memberId, departmentId);
            return Ok(belongs);
        }
    }
}
