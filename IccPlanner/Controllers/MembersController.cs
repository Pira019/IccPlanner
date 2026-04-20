using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Member;
using Application.Responses;
using Application.Responses.Errors;
using Application.Responses.Invitation;
using Application.Responses.Member;
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

        /// <summary>
        ///     Profil du membre connecté.
        /// </summary>
        [HttpGet("profile")]
        [Authorize]
        [ProducesResponseType<ProfileResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProfile()
        {
            var memberId = await GetMemberAuthIdAsync();
            var profile = await _memberService.GetProfileAsync(memberId);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        /// <summary>
        ///     Modifier le profil du membre connecté.
        /// </summary>
        [HttpPut("profile")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var memberId = await GetMemberAuthIdAsync();
            var result = await _memberService.UpdateProfileAsync(memberId, request);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            }

            return NoContent();
        }

        /// <summary>
        ///     Anniversaires du mois pour les départements du membre connecté.
        /// </summary>
        [HttpGet("birthdays/{month:int}")]
        [Authorize]
        [ProducesResponseType<List<BirthdayResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBirthdays(int month)
        {
            var memberId = await GetMemberAuthIdAsync();
            var result = await _memberService.GetBirthdaysAsync(memberId, month);
            return Ok(result);
        }
    }
}
