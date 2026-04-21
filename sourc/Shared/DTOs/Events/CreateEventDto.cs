namespace Shared.DTOs.Events
{
    public class CreateEventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        // السطر الجديد لاستقبال مكان الفعالية
        public string Location { get; set; } = string.Empty;
    }
}