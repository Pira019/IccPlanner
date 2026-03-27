using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Responses;
using Application.Responses.Program;
using Application.Responses.ServicePrg;
using Application.Services;
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
    }
}
