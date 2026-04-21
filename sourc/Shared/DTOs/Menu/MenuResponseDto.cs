namespace Shared.DTOs.Menu
{
    public record MenuResponseDto(
        int Id,
        string Name,
        string Url,
        int Order,
        int? ParentId,
        List<MenuResponseDto> SubMenus
    );
}