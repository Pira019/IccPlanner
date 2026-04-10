using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.ServiceTab;
using Application.Responses;
using Application.Responses.Errors;
using Application.Responses.ServicePrg;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize]
    public class ServicePrgController : PlannerBaseController
    {
        private readonly ITabServicePrgService _tabServicePrgService;

        public ServicePrgController(ITabServicePrgService tabServicePrgService, IAccountRepository accountRepository)
            : base(accountRepository)
        {
           _tabServicePrgService = tabServicePrgService;
        }


        // GET: api/<ServicePrgController>
        [HttpGet("{idDepart}/{dateprg}")]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GetServiceByDepart>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int idDepart, DateOnly dateprg)
        {
            var memberId = await GetMemberAuthIdAsync();
            var result = await _tabServicePrgService.GetServicePrgByDepartAsync(idDepart, dateprg, memberId);
            return Ok(result.Value);
        }

        /// <summary>
        ///     Modifier un service d'un programme.
        /// </summary>
        [HttpPut("{servicePrgId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int servicePrgId, [FromBody] UpdateServicePrgRequest request)
        {
            var memberId = await GetMemberAuthIdAsync();
            var res = await _tabServicePrgService.UpdateServicePrgAsync(servicePrgId, request, memberId, ClaimsConstants.CAN_MANANG_DEPART);

            if (!res.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(res.Error, null, null));
            }

            return NoContent();
        }

        /// <summary>
        ///     Supprimer un service d'un programme.
        /// </summary>
        [HttpDelete("{servicePrgId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int servicePrgId)
        {
            var memberId = await GetMemberAuthIdAsync();
            var res = await _tabServicePrgService.DeleteServicePrgAsync(servicePrgId, memberId, ClaimsConstants.CAN_MANANG_DEPART);

            if (!res.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(res.Error, null, null));
            }

            return NoContent();
        } 
    }
}
