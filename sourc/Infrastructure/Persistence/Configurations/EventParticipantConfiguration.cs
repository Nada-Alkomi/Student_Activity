using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        // هذا هو السطر الذي يحل المشكلة (تحديد المفتاح المركب)
        builder.HasKey(x => new { x.UserId, x.EventId });

        builder.HasOne(x => x.User)
               .WithMany(u => u.EventParticipations) // تأكد من وجود هذا الـ Collection في ApplicationUser
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Event)
               .WithMany(e => e.Participants) // تأكد من وجود هذا الـ Collection في كلاس Event
               .HasForeignKey(x => x.EventId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}