using Core.DTOs.Clubs;
using Core.Interfaces.Clubs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Services.Abstraction.Interfaces;

namespace Services.Clubs;

public class ClubService : IClubService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;

    public ClubService(ApplicationDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<IEnumerable<ClubDto>> GetAllAsync()
    {
        return await _context.Clubs
            .Select(c => new ClubDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                MembersCount = c.Members.Count
            }).ToListAsync();
    }

    public async Task<ClubDto?> GetByIdAsync(int id)
    {
        var c = await _context.Clubs
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (c == null) return null;

        return new ClubDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageUrl = c.ImageUrl,
            MembersCount = c.Members.Count
        };
    }

    public async Task<ClubDto> CreateAsync(CreateClubDto dto)
    {
        string imageUrl = string.Empty;

        if (dto.Image != null)
        {
            imageUrl = await _fileService.UploadImageAsync(dto.Image, "clubs");
        }

        var club = new Club
        {
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = imageUrl
        };

        _context.Clubs.Add(club);
        await _context.SaveChangesAsync();

        return new ClubDto
        {
            Id = club.Id,
            Name = club.Name,
            Description = club.Description,
            ImageUrl = club.ImageUrl,
            MembersCount = 0
        };
    }

    public async Task UpdateAsync(int id, CreateClubDto dto)
    {
        var club = await _context.Clubs.FindAsync(id);
        if (club != null)
        {
            if (dto.Image != null)
            {
                _fileService.DeleteImage(club.ImageUrl);
                club.ImageUrl = await _fileService.UploadImageAsync(dto.Image, "clubs");
            }

            club.Name = dto.Name;
            club.Description = dto.Description;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var club = await _context.Clubs.FindAsync(id);
        if (club != null)
        {
            _fileService.DeleteImage(club.ImageUrl);

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
        }
    }
}