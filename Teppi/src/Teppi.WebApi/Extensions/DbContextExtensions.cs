
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teppi.Data.Persistence;

namespace Teppi.WebApi.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContextConfig(
            this IServiceCollection services, IConfiguration configuration)
        {
            string? conn = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TeppiDbContext>(
                options => options.UseSqlServer(conn));

            // Configure Identity
            services.AddIdentityApiEndpoints<User>(
                    options =>
                    {
                        options.User.RequireUniqueEmail = true;
                        options.Password.RequireDigit = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequiredLength = 8;
                    })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<TeppiDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
            
            return services;
        }
    }
}