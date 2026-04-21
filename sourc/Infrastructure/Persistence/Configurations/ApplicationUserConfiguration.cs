using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.OtpCode).HasMaxLength(6);
        }
    }
}