using Core.DTOs.Activities;
using Core.Interfaces.Activities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Services.Activities;

public class ActivityCategoryService : IActivityCategoryService
{
    private readonly ApplicationDbContext _context;

    public ActivityCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityCategoryDto>> GetAllAsync()
    {
        return await _context.ActivityCategories
            .Select(c => new ActivityCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Icon = c.Icon
            }).ToListAsync();
    }

    public async Task<ActivityCategoryDto?> GetByIdAsync(int id)
    {
        var category = await _context.ActivityCategories.FindAsync(id);
        if (category == null) return null;

        return new ActivityCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Icon = category.Icon
        };
    }

    public async Task<ActivityCategoryDto> CreateAsync(ActivityCategoryDto dto)
    {
        var category = new ActivityCategory
        {
            Name = dto.Name,
            Icon = dto.Icon
        };

        _context.ActivityCategories.Add(category);
        await _context.SaveChangesAsync();

        dto.Id = category.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, ActivityCategoryDto dto)
    {
        var category = await _context.ActivityCategories.FindAsync(id);
        if (category != null)
        {
            category.Name = dto.Name;
            category.Icon = dto.Icon;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.ActivityCategories.FindAsync(id);
        if (category != null)
        {
            _context.ActivityCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}