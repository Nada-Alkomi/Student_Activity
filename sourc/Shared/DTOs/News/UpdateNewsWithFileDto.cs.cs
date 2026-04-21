using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.News
{
    public class UpdateNewsWithFileDto
    {
        public IFormFile? File { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ExistingImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
