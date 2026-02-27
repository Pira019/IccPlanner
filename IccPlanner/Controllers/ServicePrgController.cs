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
    public class ServicePrgController : ControllerBase
    {
        private readonly ITabServicePrgService _tabServicePrgService;

        public ServicePrgController(ITabServicePrgService tabServicePrgService)
        {
           _tabServicePrgService = tabServicePrgService;
        }


        // GET: api/<ServicePrgController>
        [HttpGet("{idDepart}/{month}/{year}")]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GetServiceByDepart>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int idDepart,int month, int year)
        {
            var result = await _tabServicePrgService.GetServicePrgByDepartAsync(idDepart,month,year);
            return Ok(result.Value);
        } 
    }
}
