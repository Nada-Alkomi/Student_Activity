using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Competitions;

public class CreateCompetitionDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public IFormFile? Image { get; set; }

    [Required]
    public DateTime RegistrationDeadline { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
}