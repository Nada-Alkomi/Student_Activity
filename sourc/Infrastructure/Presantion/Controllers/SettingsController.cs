using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;
using Shared.DTOs.HomeSettings;

namespace Must.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IHomeSettingService _service;

        public SettingsController(IHomeSettingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var settings = await _service.GetSettingsAsync();
            return Ok(settings);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateHomeSettingDto dto)
        {
            await _service.UpdateSettingsAsync(dto);
            return Ok(new { message = "Settings updated successfully" });
        }
    }
}