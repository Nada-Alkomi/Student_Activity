namespace Core.DTOs.Participants;

public class ParticipantRegistrationDto
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ItemTitle { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
