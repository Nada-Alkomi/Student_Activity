namespace Core.DTOs.Competitions;

public class CompetitionDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime RegistrationDeadline { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }
}