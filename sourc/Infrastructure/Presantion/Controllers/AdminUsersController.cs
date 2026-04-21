using Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminUsersController : ControllerBase
{
    private readonly IUserService _service;

    public AdminUsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _service.GetAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await _service.GetUserByIdAsync(id);
        if (result == null) return NotFound(new { message = "User not found" });
        return Ok(result);
    }

    [HttpPatch("{id}/toggle-status")]
    public async Task<IActionResult> ToggleStatus(string id)
    {
        var result = await _service.ToggleUserStatusAsync(id);

        if (!result)
            return BadRequest(new { message = "Failed to toggle user status or user not found" });

        return Ok(new { message = "User status has been toggled successfully" });
    }
}