using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class HomeSettingConfiguration : IEntityTypeConfiguration<HomeSetting>
    {
        public void Configure(EntityTypeBuilder<HomeSetting> builder)
        {
            builder.HasKey(x => x.Id);

      
            builder.Property(x => x.UniversityEmail).IsRequired().HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        }
    }
}