using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClubMemberConfiguration : IEntityTypeConfiguration<ClubMember>
{
    public void Configure(EntityTypeBuilder<ClubMember> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ClubId });

        builder.HasOne(x => x.User)
               .WithMany(u => u.ClubMemberships)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Club)
               .WithMany(c => c.Members)
               .HasForeignKey(x => x.ClubId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}