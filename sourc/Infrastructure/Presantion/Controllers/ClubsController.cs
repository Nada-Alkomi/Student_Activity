using Core.DTOs.Clubs;
using Core.Interfaces.Clubs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClubsController : ControllerBase
{
    private readonly IClubService _service;

    public ClubsController(IClubService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = "Club not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateClubDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] CreateClubDto dto)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Update failed, club not found" });

        await _service.UpdateAsync(id, dto);
        return Ok(new { message = "Club updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Delete failed, club not found" });

        await _service.DeleteAsync(id);
        return Ok(new { message = "Club deleted successfully" });
    }
}