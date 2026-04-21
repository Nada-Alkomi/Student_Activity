using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CompetitionParticipantConfiguration : IEntityTypeConfiguration<CompetitionParticipant>
{
    public void Configure(EntityTypeBuilder<CompetitionParticipant> builder)
    {
        builder.HasKey(x => new { x.UserId, x.CompetitionId });

        builder.HasOne(x => x.User)
               .WithMany(u => u.CompetitionParticipations)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Competition)
               .WithMany(c => c.Participants)
               .HasForeignKey(x => x.CompetitionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}