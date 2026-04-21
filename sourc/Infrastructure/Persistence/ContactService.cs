using Core.DTOs.Contact;
using Core.Interfaces.Contact;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Services.Contact;

public class ContactService : IContactService
{
    private readonly ApplicationDbContext _context;

    public ContactService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(CreateContactDto dto)
    {
        var msg = new ContactMessage
        {
            Name = dto.Name,
            Email = dto.Email,
            Subject = dto.Subject,
            Message = dto.Message,
            SubmittedAt = DateTime.UtcNow
        };

        _context.ContactMessages.Add(msg);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ContactMessage>> GetAllAsync()
    {
        return await _context.ContactMessages
            .OrderByDescending(x => x.SubmittedAt)
            .ToListAsync();
    }

    public async Task<string> DeleteAsync(int id)
    {
        var msg = await _context.ContactMessages.FindAsync(id);
        if (msg == null) return "Not Found";

        _context.ContactMessages.Remove(msg);
        await _context.SaveChangesAsync();
        return "Message deleted successfully";
    }
}