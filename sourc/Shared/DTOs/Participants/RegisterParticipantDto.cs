namespace Core.DTOs.Participants;

public class RegisterParticipantDto
{
    public int ItemId { get; set; }
    public string UserId { get; set; } = string.Empty;

    public int ClubId
    {
        get => ItemId;
        set => ItemId = value;
    }

    public int ActivityId
    {
        get => ItemId;
        set => ItemId = value;
    }

    public int CompetitionId
    {
        get => ItemId;
        set => ItemId = value;
    }

    public int EventId
    {
        get => ItemId;
        set => ItemId = value;
    }
}
