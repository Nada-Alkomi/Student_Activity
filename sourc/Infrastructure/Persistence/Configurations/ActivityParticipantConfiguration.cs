using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ActivityParticipantConfiguration : IEntityTypeConfiguration<ActivityParticipant>
{
    public void Configure(EntityTypeBuilder<ActivityParticipant> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ActivityId }); // مفتاح مركب

        builder.HasOne(x => x.User)
               .WithMany(u => u.ActivityParticipations)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Activity)
               .WithMany(a => a.Participants)
               .HasForeignKey(x => x.ActivityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}