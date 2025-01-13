using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests.Account;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    /// <summary>
    ///   Cette classe permet de gerer les comptes 
    /// </summary>
    /// 
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
        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.CreateAccount(request);

            if (!result.Succeeded) 
            { 
                return Ok(new { message = "Erreur", error = result.Errors });
            }


            return Ok(new { message = "OK" }); 
        }
    }
}
