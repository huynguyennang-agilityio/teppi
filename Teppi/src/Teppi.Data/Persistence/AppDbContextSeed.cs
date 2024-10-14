using Microsoft.AspNetCore.Identity;
using Teppi.Share.Entities;

namespace Teppi.Data.Persistence;

public static class AppDbContextSeed
{
    public static async Task RunMigrationAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        // Seed admin role
        var adminRole = await roleManager.FindByNameAsync(UserRoles.Admin);

        if (adminRole == null)
        {
            adminRole = new Role
            {
                Name = UserRoles.Admin
            };
            await roleManager.CreateAsync(adminRole);
        }

        // Seed user role
        var userRole = await roleManager.FindByNameAsync(UserRoles.User);

        if (userRole == null)
        {
            userRole = new Role
            {
                Name = UserRoles.User
            };
            await roleManager.CreateAsync(userRole);
        }

        // Seed admin user
        var adminEmail = "admin@example.com";
        var adminPassword = "Admin@123";
        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new User
            {
                Email = adminEmail,
                EmailConfirmed = true,
                UserName = adminEmail
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }
        }
    }
}
