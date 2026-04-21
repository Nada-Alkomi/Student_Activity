using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;
using Shared.DTOs.Sliders;

namespace Must.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _service;
        private readonly IFileService _fileService;

        public SliderController(ISliderService service, IFileService fileService)
        {
            _service = service;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateSliderWithFileDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Image file is required.");

            var imageUrl = await _fileService.UploadImageAsync(dto.File, "sliders");

            var createDto = new CreateSliderDto(imageUrl, dto.Title, dto.SubTitle, dto.Order);

            var result = await _service.AddAsync(createDto);

            return Ok(result);
        }

        [HttpPatch("{id}/toggle")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Toggle(int id)
        {
            var result = await _service.ToggleStatusAsync(id);

            if (result == null) return NotFound();

            return Ok(new
            {
                message = result.Value.Message,
                data = result.Value.Data
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (result == null) return NotFound();

            return Ok(new { message = result });
        }
    }

    public class CreateSliderWithFileDto
    {
        public IFormFile File { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public int Order { get; set; }
    }
}