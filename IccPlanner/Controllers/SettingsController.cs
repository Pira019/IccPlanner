using Application.Constants;
using Application.Interfaces.Services;
using Application.Responses.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    /// <summary>
    ///     Gestion des paramètres de l'application.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly IAppSettingEntryService _settingService;

        public SettingsController(IAppSettingEntryService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        ///     Récupère les paramètres de délai.
        /// </summary>
        [HttpGet("deadlines")]
        [ProducesResponseType<DeadlineSettingsResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDeadlines()
        {
            var result = await _settingService.GetDeadlineSettingsAsync();
            return Ok(result);
        }

        /// <summary>
        ///     Sauvegarde les paramètres de délai. (Admin seulement)
        /// </summary>
        [HttpPut("deadlines")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveDeadlines([FromBody] SaveDeadlineSettingsRequest request)
        {
            await _settingService.SaveDeadlineSettingsAsync(request);
            return Ok();
        }

        /// <summary>
        ///     Supprime une règle de délai. (Admin seulement)
        /// </summary>
        [HttpDelete("deadlines/{id:int}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRule(int id)
        {
            await _settingService.DeleteRuleAsync(id);
            return NoContent();
        }
    }
}
