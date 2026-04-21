using Core.DTOs.Competitions;
using Core.Interfaces.Competitions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Services.Abstraction.Interfaces;

namespace Services.Competitions;

public class CompetitionService : ICompetitionService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;

    public CompetitionService(ApplicationDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<IEnumerable<CompetitionDto>> GetAllAsync()
    {
        return await _context.Competitions
            .Where(c => c.IsActive)
            .OrderByDescending(c => c.StartDate)
            .Select(c => new CompetitionDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                RegistrationDeadline = c.RegistrationDeadline,
                StartDate = c.StartDate,
                IsActive = c.IsActive
            }).ToListAsync();
    }

    public async Task<CompetitionDto?> GetByIdAsync(int id)
    {
        var c = await _context.Competitions.FindAsync(id);
        if (c == null) return null;

        return new CompetitionDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            ImageUrl = c.ImageUrl,
            RegistrationDeadline = c.RegistrationDeadline,
            StartDate = c.StartDate,
            IsActive = c.IsActive
        };
    }

    public async Task<CompetitionDto> CreateAsync(CreateCompetitionDto dto)
    {
        string imageUrl = string.Empty;

        if (dto.Image != null)
        {
            imageUrl = await _fileService.UploadImageAsync(dto.Image, "competitions");
        }

        var comp = new Competition
        {
            Title = dto.Title,
            Description = dto.Description,
            ImageUrl = imageUrl,
            RegistrationDeadline = dto.RegistrationDeadline,
            StartDate = dto.StartDate,
            IsActive = true
        };

        _context.Competitions.Add(comp);
        await _context.SaveChangesAsync();

        return new CompetitionDto
        {
            Id = comp.Id,
            Title = comp.Title,
            Description = comp.Description,
            ImageUrl = comp.ImageUrl,
            RegistrationDeadline = comp.RegistrationDeadline,
            StartDate = comp.StartDate,
            IsActive = comp.IsActive
        };
    }

    public async Task UpdateAsync(int id, CreateCompetitionDto dto)
    {
        var comp = await _context.Competitions.FindAsync(id);
        if (comp != null)
        {
            if (dto.Image != null)
            {
                _fileService.DeleteImage(comp.ImageUrl);
                comp.ImageUrl = await _fileService.UploadImageAsync(dto.Image, "competitions");
            }

            comp.Title = dto.Title;
            comp.Description = dto.Description;
            comp.RegistrationDeadline = dto.RegistrationDeadline;
            comp.StartDate = dto.StartDate;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var comp = await _context.Competitions.FindAsync(id);
        if (comp != null)
        {
            _fileService.DeleteImage(comp.ImageUrl);

            _context.Competitions.Remove(comp);
            await _context.SaveChangesAsync();
        }
    }
}
