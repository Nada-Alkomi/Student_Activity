using Core.DTOs.Users;
using Core.Interfaces.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Services.Users;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync()
    {
        return await _userManager.Users
            .Select(u => new UserSummaryDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email ?? string.Empty,
                UserName = u.UserName ?? string.Empty,
                IsActive = u.IsOtpVerified
            }).ToListAsync();
    }

    public async Task<UserSummaryDto?> GetUserByIdAsync(string userId)
    {
        var u = await _userManager.FindByIdAsync(userId);
        if (u == null) return null;

        return new UserSummaryDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            FullName = u.FirstName + " " + u.LastName,
            Email = u.Email ?? string.Empty,
            UserName = u.UserName ?? string.Empty,
            IsActive = u.IsOtpVerified
        };
    }

    public async Task<bool> ToggleUserStatusAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        user.IsOtpVerified = !user.IsOtpVerified;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}
