using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses;
using Application.Responses.Errors;
using Domain.Abstractions;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IccPlanner.Controllers
{
    /// <summary>
    /// Admin controlleur
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAccountService _accountService;
        private readonly IRecurrentDateService _recurrentDateService;
        private readonly IPlanningArchiveService _planningArchiveService;
        private readonly int _defaultDaysAhead;

        public AdminController(
            ILogger<AdminController> logger,
            IAccountService accountService,
            IRecurrentDateService recurrentDateService,
            IPlanningArchiveService planningArchiveService,
            IOptions<AppSetting> appSettings)
        {
            _logger = logger;
            _accountService = accountService;
            _recurrentDateService = recurrentDateService;
            _planningArchiveService = planningArchiveService;
            _defaultDaysAhead = appSettings.Value.Parametres?.RecurrentDaysAhead ?? 30;
        }

        // POST api/<AdminController>
        /// <summary>
        /// Permet de creer un compte Admin
        /// </summary>
        /// <param name="request"> Model de body <see cref="CreateAccountRequest"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IActionResult"/> de l'opération </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdminAccount(CreateAccountRequest request)
        {
            try
            {
                var isAdminUsersExist = await _accountService.IsAdminExistsAsync();

                if (isAdminUsersExist)
                {
                    //return BadRequest(AccountResponseError.AdminUserExist());
                    return BadRequest();
                }
                var result = await _accountService.CreateAccount(request,true);

                if (!result.Succeeded)
                {
                     var response = AccountResponseError.ApiIdentityResultResponseError(result);
                     return BadRequest(response); 
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, AccountErrors.CREATE_ADMIN_ERROR.Message);
                 return BadRequest(AccountResponseError.InternalServerError(AccountErrors.CREATE_ADMIN_ERROR.Message)); 
            }
        }

        /// <summary>
        ///     Exécuter manuellement la génération des dates récurrentes.
        /// </summary>
        [HttpPost("generate-recurrent-dates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateRecurrentDates()
        {
            var totalCreated = await _recurrentDateService.GenerateRecurrentDatesAsync(_defaultDaysAhead);
            return Ok(new { datesCreated = totalCreated });
        }

        /// <summary>
        ///     Exécuter manuellement l'archivage des plannings passés.
        /// </summary>
        [HttpPost("archive-past-plannings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ArchivePastPlannings()
        {
            var totalArchived = await _planningArchiveService.ArchivePastPeriodsAsync();
            return Ok(new { periodsArchived = totalArchived });
        }
    }
}
