namespace Shared.DTOs.Events
{
    public class UpdateEventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        // السطر المضاف لتعديل مكان الفعالية
        public string Location { get; set; } = string.Empty;
    }
}