namespace Shared.DTOs.News
{
    public record NewsResponseDto(
        int Id,
        string Title,
        string Content,
        string ImageUrl,
        DateTime CreatedAt
    );
}