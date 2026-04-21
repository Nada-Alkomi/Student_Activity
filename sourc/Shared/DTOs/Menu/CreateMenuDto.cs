namespace Shared.DTOs.Menu
{
    public record CreateMenuDto(
        string Name,
        string Url,
        int Order,
        int? ParentId
    );
}