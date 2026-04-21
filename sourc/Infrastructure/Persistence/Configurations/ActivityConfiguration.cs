using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Duration).HasMaxLength(50);
        builder.Property(x => x.PlayersCount).HasMaxLength(50);

        // العلاقة مع القسم (Category)
        builder.HasOne(x => x.Category)
               .WithMany(c => c.Activities)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict); // منع الحذف التلقائي
    }
}