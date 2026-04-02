using Application.Interfaces.Services;
using Application.Requests.Poste;
using Application.Responses;
using Application.Responses.Department;
using Application.Responses.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostesController : ControllerBase
    {
        private readonly IPosteService _posteService;

        public PostesController(IPosteService posteService)
        {
            _posteService = posteService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType<List<PosteResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var postes = await _posteService.GetAllAsync();
            return Ok(postes);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType<PosteResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _posteService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(ApiError.ErrorMessage(result.Error, null, null));
            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType<PosteResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddPosteRequest request)
        {
            var result = await _posteService.AddAsync(request);
            if (!result.IsSuccess)
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            return Created(string.Empty, result.Value);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType<PosteResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, AddPosteRequest request)
        {
            var result = await _posteService.UpdateAsync(id, request);
            if (!result.IsSuccess)
                return BadRequest(ApiError.ErrorMessage(result.Error, null, null));
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ApiErrorResponseModel>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _posteService.DeleteAsync(id);
            if (!result.IsSuccess)
                return NotFound(ApiError.ErrorMessage(result.Error, null, null));
            return NoContent();
        }
    }
}
