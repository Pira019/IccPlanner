using Application.Interfaces.Services;
using Application.Requests.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost] 
        public IActionResult createUser(CreateUserRequest request)
        {
            if(!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }          
            try
            {
                _userService.CreateUser(request);
                return Ok(new { Message = "Utilisateur créé avec succès." });
            }
            catch(Exception ex)  
            {
                    return StatusCode(500, new { Message = "Erreur", Error = ex.Message });
            }

        }
    }
}
