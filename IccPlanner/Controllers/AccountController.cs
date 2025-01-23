using Application.Interfaces.Services;
using Application.Requests.Account;
using Application.Responses;
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
        [ProducesResponseType<ApiErrorResponse> (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            var result = await _accountService.CreateAccount(request);

            if (!result.Succeeded)
            {
                var response = CreateAccountResponseError.ApiErrorResponse(result);
                return BadRequest(response);
            }
            return Ok();
        }
    }
}
