﻿using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses;
using Application.Responses.Account;
using Application.Responses.Errors;
using Infrastructure.Configurations.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    /// <summary>
    /// Cette classe permet de gérer les comptes 
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        // POST: AccountsController/Create
        /// <summary>
        /// Ajouter un membre
        /// </summary>
        /// <param name="request"> 
        /// <see cref="CreateAccountRequest"/> </param>
        /// <returns>Réponse object IActionResult</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateAccountRequest request)
        {
            var result = await _accountService.CreateAccount(request);

            if (!result.Succeeded)
            {
                var response = AccountResponseError.ApiIdentityResultResponseError(result);
                return BadRequest(response);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// 
        /// Confirmer l’émail
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
                : BadRequest(AccountResponseError.ApiIdentityResultResponseError(result));
        }

        /// <summary>
        /// Authentifier un utilisateur
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType<LoginAccountResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromForm] LoginRequest request, ITokenProvider tokenProvider)
        {
            // Authentification
            var resultat = await _accountService.Login(request);

            if (resultat.IsLockedOut)
            {
                return BadRequest(AccountResponseError.UserIsLockedOut());
            }

            if (!resultat.Succeeded)
            {
                return BadRequest(AccountResponseError.LoginInvalidAttempt());
            }

            var userAuth = await _accountService.FindUserAccountByEmail(request.Email);
            var userAuthRoles = await _accountService.GetUserRoles(userAuth!);
            var token = tokenProvider.Create(userAuth!, userAuthRoles);

            var res = new LoginAccountResponse
            {
                AccessToken = token
            };

            return Ok(res);

        }
    }
}
