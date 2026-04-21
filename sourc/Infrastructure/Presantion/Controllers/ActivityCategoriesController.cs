using Core.DTOs.Activities;
using Core.Interfaces.Activities;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityCategoriesController : ControllerBase
{
    private readonly IActivityCategoryService _service;

    public ActivityCategoriesController(IActivityCategoryService service)
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
        if (result == null) return NotFound(new { message = "Category not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ActivityCategoryDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ActivityCategoryDto dto)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Update failed, category not found" });

        await _service.UpdateAsync(id, dto);
        return Ok(new { message = "Category updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound(new { message = "Delete failed, category not found" });

        await _service.DeleteAsync(id);
        return Ok(new { message = "Category deleted successfully" });
    }
}