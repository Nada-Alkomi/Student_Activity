namespace Shared.DTOs.Menu
{
    public record UpdateMenuDto(
        string Name,
        string Url,
        int Order,
        int? ParentId
    );
}