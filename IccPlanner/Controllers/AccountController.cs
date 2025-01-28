using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses;
using Application.Responses.Account;
using Application.Responses.Errors;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    /// <summary>
    /// Cette classe permet de gerer les comptes 
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST: AccountController/Create
        /// <summary>
        /// Ajouter un membre
        /// </summary>
        /// <param name="request"> 
        /// <see cref="CreateAccountRequest"/> </param>
        /// <returns>Reponse object IActionResult</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateAccountRequest request)
        {
            var result = await _accountService.CreateAccount(request);

            if (!result.Succeeded)
            {
                var response = AccountResponseError.ApiErrorResponse(result);
                return BadRequest(response);
            }
            return Ok();
        }

        /// <summary>
        /// Confrimer l'email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            // Trouver l'utilisateur
            var user = await _accountService.FindUserAccountById(request.UserId);

            if (user == null)
            {
                return BadRequest(AccountResponseError.UserNotFound());
            }

            var result = await _accountService.ConfirmEmailAccount(user, request.Token);

            return result.Succeeded
                ? Ok()
                : BadRequest(AccountResponseError.ApiErrorResponse(result));
        }

        /// <summary>
        /// Authentifier un utilisateur
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType<LoginAccountResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            // Auth
            var resultat = await _accountService.Login(request);

            if (resultat.IsLockedOut)
            {
                return BadRequest(AccountResponseError.UserIsLockedOut());
            }

            if (!resultat.Succeeded)
            {
                return BadRequest(AccountResponseError.LoginInvalidAttempt());
            }

            var res = new LoginAccountResponse
            {
                AccessToken = "Vlider ohhh"
            };

            return Ok(res);
        }
    }


}
