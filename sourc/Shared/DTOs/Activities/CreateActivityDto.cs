using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Activities;

public class CreateActivityDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public IFormFile? Image { get; set; }

    public string? Duration { get; set; }

    public string? PlayersCount { get; set; }

    [Required]
    public int CategoryId { get; set; }
}