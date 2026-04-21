using Core.DTOs.Competitions;
using Core.Interfaces.Competitions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompetitionsController : ControllerBase
{
    private readonly ICompetitionService _service;

    public CompetitionsController(ICompetitionService service)
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
        if (result == null) return NotFound(new { message = "Competition not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateCompetitionDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] CreateCompetitionDto dto)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Update failed, competition not found" });

        await _service.UpdateAsync(id, dto);
        return Ok(new { message = "Competition updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Delete failed, competition not found" });

        await _service.DeleteAsync(id);
        return Ok(new { message = "Competition deleted successfully" });
    }
}