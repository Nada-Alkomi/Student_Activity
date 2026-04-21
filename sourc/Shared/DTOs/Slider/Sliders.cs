namespace Shared.DTOs.Sliders
{
    public record CreateSliderDto(string ImageUrl, string? Title, string? SubTitle, int Order);
    public record SliderResponseDto(int Id, string ImageUrl, string? Title, string? SubTitle, int Order, bool IsActive);
}