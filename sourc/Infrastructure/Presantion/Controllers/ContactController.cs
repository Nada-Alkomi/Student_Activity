using Core.DTOs.Contact;
using Core.Interfaces.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Send(CreateContactDto dto)
    {
        var result = await _service.CreateAsync(dto);
        if (!result) return BadRequest("Failed to send message");

        return Ok(new
        {
            message = "Message sent successfully",
            data = result
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (result == "Not Found") return NotFound();

        return Ok(new { message = result });
    }
}