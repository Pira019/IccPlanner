// Ignore Spelling: Prg

using Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Application.Requests.ServiceTab;
using Application.Interfaces.Services;
using Application.Responses.Errors;
using Shared.Ressources;
using Application.Responses.TabService;
using Application.Interfaces.Repositories;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : PlannerBaseController
    {
        private IServiceTabService _serviceTabService;
        private ITabServicePrgService _tabServicePrgService; 
        private IDepartmentRepository _departmentRepository;

        public ServicesController(IAccountRepository accountRepository, IServiceTabService serviceTabService, ITabServicePrgService tabServicePrgService,
            IDepartmentRepository departmentRepository) : base(accountRepository)
        {
            _serviceTabService = serviceTabService;
            _tabServicePrgService = tabServicePrgService; 
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        ///     Ajouter un service
        /// </summary>
        /// <param name="serviceRequest">
        ///     Modèle de donnée a recevoir 
        /// </param>
        /// <returns></returns>
        [HttpPost] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseAddResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] AddServiceRequest serviceRequest)
        {
            var resultat = await _serviceTabService.Add(serviceRequest);

            if (!resultat.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(resultat.Error, null, null));
            } 
            return Created(string.Empty, resultat.Value);
        }

        /// <summary>
        ///     Obtenir tous les services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<GetTabServiceListResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _serviceTabService.GetAll());
        }

        [HttpGet("dates/{month:int}/{year:int}")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<GetDatesResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceDate(int? month,int? year)
        { 
            return Ok(await _tabServicePrgService.GetDates(await GetMemberAuthIdAsync(), month, year));
        }

        [HttpGet("department-services/{datePrg}")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<GetServicesListResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceByDate(DateOnly datePrg)
        {
            return Ok( await _departmentRepository.GetDepartmentServicesByDate(await GetMemberAuthIdAsync(), datePrg));
        }


        /// <summary>
        ///      Ajouter un service d'un programme d'un département
        /// </summary>
        /// <param name="servicePrgDepartmentRequest">
        ///     Modèle de donnée a recevoir 
        /// </param>
        /// <returns></returns>
        [HttpPost("program-department")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseAddResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddServicePrgDepartment([FromBody] AddServicePrgDepartmentRequest servicePrgDepartmentRequest)
        {
            var res = await _tabServicePrgService.AddServicePrg(servicePrgDepartmentRequest);

            if (!res.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(res.Error, null, null));
            } 
            return Created(string.Empty, string.Empty);
        }

        /// <summary>
        ///     Recherche des programme
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("search-programs")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<GetServicesListResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPrgDepartment([FromBody] ServicesRequest request)
        {
            var res = await _tabServicePrgService.GetPrgServices(request);

            if (!res.IsSuccess)
            {
                return BadRequest(ApiError.ErrorMessage(res.Error, null, null));
            }
            return Ok(res.Value);
        }
    }
}
