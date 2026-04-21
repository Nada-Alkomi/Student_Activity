namespace Core.DTOs.Activities;

public class ActivityDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string? Duration { get; set; }
    public string? PlayersCount { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public int ActivityCategoryId => CategoryId;
    public string? NumberOfPlayers => PlayersCount;
}
