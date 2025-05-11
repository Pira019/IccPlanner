using Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Application.Requests.ServiceTab;
using Application.Interfaces.Services;
using Application.Responses.Errors;
using Shared.Ressources;
using Application.Responses.TabService;


namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private IServiceTabService _serviceTabService;

        public ServicesController(IServiceTabService serviceTabService)
        {
            _serviceTabService = serviceTabService;
        }

        /// <summary>
        ///     Ajouter un service
        /// </summary>
        /// <param name="serviceRequest">
        ///     Modèle de donnée a recevoir 
        /// </param>
        /// <returns></returns>
        [HttpPost]
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
                return BadRequest(ApiError.ErrorMessage(string.Format(ValidationMessages.SERVICE_EXISTS,serviceRequest.DisplayName, serviceRequest.StartTime, serviceRequest.EndTime), null, null));
            }

            var resultat = await _serviceTabService.Add(serviceRequest);
            return Created(string.Empty, resultat);
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
    }
}
