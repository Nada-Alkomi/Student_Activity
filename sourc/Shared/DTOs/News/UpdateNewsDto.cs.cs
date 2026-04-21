namespace Shared.DTOs.News
{
    public record UpdateNewsDto(
        string Title,
        string Content,
        string ImageUrl,
        DateTime CreatedAt
    );
}
