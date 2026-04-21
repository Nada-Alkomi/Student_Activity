namespace Shared.DTOs.News
{
    public record CreateNewsDto(
        string Title,
        string Content,
        string ImageUrl,
        DateTime CreatedAt
    );
}
