namespace Shared.DTOs.Events
{
    public class EventResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        // السطر المضاف لعرض مكان الفعالية
        public string Location { get; set; } = string.Empty;
    }
}