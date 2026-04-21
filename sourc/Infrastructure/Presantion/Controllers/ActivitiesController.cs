using Core.DTOs.Activities;
using Core.Interfaces.Activities;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _service;

    public ActivitiesController(IActivityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("Category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        return Ok(await _service.GetByCategoryIdAsync(categoryId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = "Activity not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateActivityDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateActivityDto dto)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Update failed, activity not found" });

        await _service.UpdateAsync(id, dto);
        return Ok(new { message = "Activity updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Delete failed, activity not found" });

        await _service.DeleteAsync(id);
        return Ok(new { message = "Activity deleted successfully" });
    }
}