// Ignore Spelling: Prg

using Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Requests.ServiceTab;
using Application.Interfaces.Services;
using Application.Responses.Errors;
using Application.Responses.TabService;
using Application.Interfaces.Repositories;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : PlannerBaseController
    {
        private readonly IServiceTabService _serviceTabService;
        private readonly ITabServicePrgService _tabServicePrgService;
        private readonly IDepartmentService _departmentService;

        public ServicesController(
            IAccountRepository accountRepository,
            IServiceTabService serviceTabService,
            ITabServicePrgService tabServicePrgService,
            IDepartmentService departmentService) : base(accountRepository)
        {
            _serviceTabService = serviceTabService;
            _tabServicePrgService = tabServicePrgService;
            _departmentService = departmentService;
        }

        /// <summary>
        ///     Ajouter un service
        /// </summary>
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
        [HttpGet]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<IEnumerable<GetTabServiceListResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _serviceTabService.GetAll());
        }

        /// <summary>
        ///     Obtenir les dates de services par département
        /// </summary>
        [HttpGet("dates/{month:int}/{year:int}/{idDepartment:int}")]
        [Authorize]
        [ProducesResponseType<IEnumerable<GetDatesResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceDate(int month, int year, int idDepartment)
        {
            var req = await _tabServicePrgService.GetDatesByDepartAsync(month, year, idDepartment);
            return Ok(req.Value);
        }

        /// <summary>
        ///     Obtenir les services groupés par département pour une date donnée
        /// </summary>
        [HttpGet("department-services/{datePrg}")]
        [Authorize]
        [ProducesResponseType<List<DepartmentServicesResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceByDate(DateOnly datePrg)
        {
            return Ok(await _departmentService.GetDepartmentServicesByDateAsync(datePrg));
        }

        /// <summary>
        ///     Ajouter un service d'un programme d'un département
        /// </summary>
        [HttpPost("program-department")]
        [Authorize]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
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
        ///     Recherche des programmes
        /// </summary>
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
