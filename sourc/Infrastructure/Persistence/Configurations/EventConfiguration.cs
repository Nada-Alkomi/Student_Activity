using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Description)
                   .IsRequired();

            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(x => x.EventDate)
                   .IsRequired();

            // السطر الجديد لمكان الفعالية
            builder.Property(x => x.Location)
                   .IsRequired()       // لو عاوزه إجباري
                   .HasMaxLength(250); // طول مناسب لاسم مكان أو عنوان
        }
    }
}