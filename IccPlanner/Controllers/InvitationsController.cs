using Application;
using Application.Interfaces.Services;
using Application.Requests.Invitation;
using Application.Responses;
using Application.Responses.Department;
using Application.Responses.Errors;
using Application.Responses.Invitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ressources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationsController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }


        /// <summary>
        ///     Endpoint to send an invitation
        /// </summary>
        /// <param name="request"></param> 
        // POST api/<InvitationsController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GetDepartResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] SendRequest request)
        {
            var response = await _invitationService.SendInvitationAnsyc(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return Created(string.Empty, string.Empty);
        } 

        [HttpGet("{id}")] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<InvitationResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _invitationService.FindValidInviation(id);
            if (!response.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(response.Error, null, null));
            }
            return Ok(response.Value);
        } 
    }
}
