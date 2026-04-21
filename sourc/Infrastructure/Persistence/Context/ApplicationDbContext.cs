using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomeSetting> HomeSettings { get; set; } 
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        public DbSet<ClubMember> ClubMembers { get; set; }
        public DbSet<CompetitionParticipant> CompetitionParticipants { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}