using Core.DTOs.Participants;
using Core.Interfaces.Participants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Services.Participants;

public class ParticipantService : IParticipantService
{
    private readonly ApplicationDbContext _context;

    public ParticipantService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> JoinClubAsync(RegisterParticipantDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        var clubExists = await _context.Clubs.AnyAsync(c => c.Id == dto.ItemId);

        if (!userExists || !clubExists) return false;

        var exists = await _context.ClubMembers.AnyAsync(x => x.ClubId == dto.ItemId && x.UserId == dto.UserId);
        if (exists) return false;

        _context.ClubMembers.Add(new ClubMember { ClubId = dto.ItemId, UserId = dto.UserId });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RegisterForActivityAsync(RegisterParticipantDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        var activityExists = await _context.Activities.AnyAsync(a => a.Id == dto.ItemId);

        if (!userExists || !activityExists) return false;

        var exists = await _context.ActivityParticipants.AnyAsync(x => x.ActivityId == dto.ItemId && x.UserId == dto.UserId);
        if (exists) return false;

        _context.ActivityParticipants.Add(new ActivityParticipant { ActivityId = dto.ItemId, UserId = dto.UserId });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RegisterForCompetitionAsync(RegisterParticipantDto dto)
    {
        var validationMessage = await ValidateCompetitionRegistrationAsync(dto);
        if (validationMessage != null) return false;

        _context.CompetitionParticipants.Add(new CompetitionParticipant { CompetitionId = dto.ItemId, UserId = dto.UserId });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<string?> ValidateCompetitionRegistrationAsync(RegisterParticipantDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.UserId))
        {
            return "No logged-in user was found for this request.";
        }

        if (dto.ItemId <= 0)
        {
            return "Competition id is missing or invalid.";
        }

        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists)
        {
            return "The logged-in user does not exist in the database.";
        }

        var competitionExists = await _context.Competitions.AnyAsync(c => c.Id == dto.ItemId);
        if (!competitionExists)
        {
            return "The selected competition does not exist.";
        }

        var exists = await _context.CompetitionParticipants.AnyAsync(x => x.CompetitionId == dto.ItemId && x.UserId == dto.UserId);
        if (exists)
        {
            return "You are already registered for this competition.";
        }

        return null;
    }

    public async Task<bool> RegisterForEventAsync(RegisterParticipantDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        var eventExists = await _context.Events.AnyAsync(e => e.Id == dto.ItemId);

        if (!userExists || !eventExists) return false;

        var exists = await _context.EventParticipants.AnyAsync(x => x.EventId == dto.ItemId && x.UserId == dto.UserId);
        if (exists) return false;

        _context.EventParticipants.Add(new EventParticipant { EventId = dto.ItemId, UserId = dto.UserId });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ParticipantRegistrationDto>> GetActivityRegistrationsAsync()
    {
        return await _context.ActivityParticipants
            .Include(x => x.User)
            .Include(x => x.Activity)
            .OrderByDescending(x => x.RegisteredAt)
            .Select(x => new ParticipantRegistrationDto
            {
                UserId = x.UserId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email ?? string.Empty,
                ItemTitle = x.Activity.Title,
                RegisteredAt = x.RegisteredAt
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ParticipantRegistrationDto>> GetClubRegistrationsAsync()
    {
        return await _context.ClubMembers
            .Include(x => x.User)
            .Include(x => x.Club)
            .OrderByDescending(x => x.JoinedAt)
            .Select(x => new ParticipantRegistrationDto
            {
                UserId = x.UserId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email ?? string.Empty,
                ItemTitle = x.Club.Name,
                RegisteredAt = x.JoinedAt
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ParticipantRegistrationDto>> GetCompetitionRegistrationsAsync()
    {
        return await _context.CompetitionParticipants
            .Include(x => x.User)
            .Include(x => x.Competition)
            .OrderByDescending(x => x.RegisteredAt)
            .Select(x => new ParticipantRegistrationDto
            {
                UserId = x.UserId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email ?? string.Empty,
                ItemTitle = x.Competition.Title,
                RegisteredAt = x.RegisteredAt
            })
            .ToListAsync();
    }
}
