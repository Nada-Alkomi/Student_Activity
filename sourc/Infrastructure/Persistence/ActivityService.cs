using Core.DTOs.Activities;
using Core.Interfaces.Activities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Services.Abstraction.Interfaces;

namespace Services.Activities;

public class ActivityService : IActivityService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;

    public ActivityService(ApplicationDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<IEnumerable<ActivityDto>> GetAllAsync()
    {
        return await _context.Activities
            .Include(a => a.Category)
            .Select(a => new ActivityDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Duration = a.Duration,
                PlayersCount = a.PlayersCount,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name
            }).ToListAsync();
    }

    public async Task<IEnumerable<ActivityDto>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Activities
            .Where(a => a.CategoryId == categoryId)
            .Include(a => a.Category)
            .Select(a => new ActivityDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Duration = a.Duration,
                PlayersCount = a.PlayersCount,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name
            }).ToListAsync();
    }

    public async Task<ActivityDto?> GetByIdAsync(int id)
    {
        var a = await _context.Activities
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (a == null) return null;

        return new ActivityDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            ImageUrl = a.ImageUrl,
            Duration = a.Duration,
            PlayersCount = a.PlayersCount,
            CategoryId = a.CategoryId,
            CategoryName = a.Category.Name
        };
    }

    public async Task<ActivityDto> CreateAsync(CreateActivityDto dto)
    {
        string imageUrl = string.Empty;

        if (dto.Image != null)
        {
            imageUrl = await _fileService.UploadImageAsync(dto.Image, "activities");
        }

        var activity = new Activity
        {
            Title = dto.Title,
            Description = dto.Description,
            ImageUrl = imageUrl,
            Duration = dto.Duration,
            PlayersCount = dto.PlayersCount,
            CategoryId = dto.CategoryId
        };

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(activity.Id) ?? throw new Exception("Creation failed");
    }

    public async Task UpdateAsync(int id, UpdateActivityDto dto)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity != null)
        {
            if (dto.Image != null)
            {
                _fileService.DeleteImage(activity.ImageUrl);
                activity.ImageUrl = await _fileService.UploadImageAsync(dto.Image, "activities");
            }

            activity.Title = dto.Title;
            activity.Description = dto.Description;
            activity.Duration = dto.Duration;
            activity.PlayersCount = dto.PlayersCount;
            activity.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity != null)
        {
            _fileService.DeleteImage(activity.ImageUrl);

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
        }
    }
}