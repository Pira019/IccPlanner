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
        private ITabServicePrgRepository _tabServicePrgRepository;
        private IDepartmentRepository _departmentRepository;

        public ServicesController(IAccountRepository accountRepository, IServiceTabService serviceTabService, ITabServicePrgService tabServicePrgService,
            ITabServicePrgRepository tabServicePrgRepository, IDepartmentRepository departmentRepository) : base(accountRepository)
        {
            _serviceTabService = serviceTabService;
            _tabServicePrgService = tabServicePrgService;
            _tabServicePrgRepository = tabServicePrgRepository;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        ///     Ajouter un service
        /// </summary>
        /// <param name="serviceRequest">
        ///     Modèle de donnée a recevoir 
        /// </param>
        /// <returns></returns>
       /* [HttpPost]
        [Authorize(Policy = PolicyConstants.MANAGER_SERVICE)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseAddResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] AddServiceRequest serviceRequest)
        {
            var isServiceExist = await _serviceTabService.IsServiceExist(serviceRequest.StartTime, serviceRequest.EndTime, serviceRequest.DisplayName);

            if (isServiceExist)
            {
                return BadRequest(ApiError.ErrorMessage(string.Format(ValidationMessages.SERVICE_EXISTS,
                        serviceRequest.DisplayName, serviceRequest.StartTime, serviceRequest.EndTime), null, null));
            }

          //  var resultat = await _serviceTabService.Add(serviceRequest);
            return Created(string.Empty, resultat);
        }*/

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
        [Authorize(Policy = PolicyConstants.MANAGER_SERVICE)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseAddResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddServicePrgDepartment([FromBody] AddServicePrgDepartmentRequest servicePrgDepartmentRequest, [FromServices] IServiceRepository serviceRepository, [FromServices] IPrgDateRepository prgDateRepository)
        {
            //Vérifier si le service est valide
            if (!await serviceRepository.IsExist(servicePrgDepartmentRequest.ServiceId))
            {
                return BadRequest(ApiError.ErrorMessage(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.SERVICE_), null, null));
            }

            // Vérifier si Id Prg est correcte
            if (!await prgDateRepository.IsExist(servicePrgDepartmentRequest.PrgDateId))
            {
                return BadRequest(ApiError.ErrorMessage(string.Format(ValidationMessages.NOT_EXIST, ValidationMessages.PROGRAM_), null, null));
            }

            if (await _tabServicePrgRepository.IsServicePrgExist(servicePrgDepartmentRequest.ServiceId, servicePrgDepartmentRequest.PrgDateId))
            {
                return BadRequest(ApiError.ErrorMessage(ValidationMessages.PGM_SERVICE_EXIST, null, null));
            }

            await _tabServicePrgService.AddServicePrg(servicePrgDepartmentRequest);
            return Created(string.Empty, string.Empty);
        }
    }
}
