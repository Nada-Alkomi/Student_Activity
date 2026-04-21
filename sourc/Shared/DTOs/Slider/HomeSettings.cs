namespace Shared.DTOs.HomeSettings
{
    public record UpdateHomeSettingDto(string UniversityEmail, string PhoneNumber, string FacebookLink, string Address, string MapLocationUrl);
    public record HomeSettingResponseDto(string UniversityEmail, string PhoneNumber, string FacebookLink, string Address, string MapLocationUrl);
}