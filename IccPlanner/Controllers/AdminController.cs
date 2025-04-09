using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses;
using Application.Responses.Errors;
using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

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

        public AdminController(ILogger<AdminController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
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
                    return BadRequest();
                   // return BadRequest(AccountResponseError.AdminUserExist());
                }
                var result = await _accountService.CreateAccount(request,true);

                if (!result.Succeeded)
                {
                   // var response = AccountResponseError.ApiIdentityResultResponseError(result);
                   // return BadRequest(response);
                    return BadRequest();
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, AccountErrors.CREATE_ADMIN_ERROR.Message);
                //return BadRequest(AccountResponseError.InternalServerError(AccountErrors.CREATE_ADMIN_ERROR.Message));
                return BadRequest();
            }
        }
    }
}
