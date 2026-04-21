using Core.Interfaces.Activities;
using Core.Interfaces.Clubs;
using Core.Interfaces.Competitions;
using Core.Interfaces.Contact;
using Core.Interfaces.Participants;
using Core.Interfaces.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;
using Services.Abstraction.Repositories;
using Services.Activities;
using Services.Clubs;
using Services.Competitions;
using Services.Contact;
using Services.Participants;
using Services.Users;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IActivityCategoryService, ActivityCategoryService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<ICompetitionService, CompetitionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IParticipantService, ParticipantService>();

            return services;
        }
    }
}