using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;
using Shared.DTOs.News;

namespace Must.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;
        private readonly IFileService _fileService;

        public NewsController(INewsService service, IFileService fileService)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateNewsWithFileDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Image is required");

            var imageUrl = await _fileService.UploadImageAsync(dto.File, "news");

            var createDto = new CreateNewsDto(dto.Title, dto.Content, imageUrl, dto.CreatedAt);

            var result = await _service.CreateAsync(createDto);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateNewsWithFileDto dto)
        {
            string imageUrl = dto.ExistingImageUrl;

            if (dto.File != null && dto.File.Length > 0)
            {
                imageUrl = await _fileService.UploadImageAsync(dto.File, "news");
            }

            var updateDto = new UpdateNewsDto(dto.Title, dto.Content, imageUrl, dto.CreatedAt);

            var result = await _service.UpdateAsync(id, updateDto);

            if (result == null) return NotFound();

            return Ok(result);
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
}
