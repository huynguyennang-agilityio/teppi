using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Teppi.Share.Entities;

namespace Teppi.Data.Persistence;

public class TeppiDbContext(
    DbContextOptions<TeppiDbContext> options) :
    IdentityDbContext<User, Role, string>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserRole>(userRole =>
        {
            userRole.HasOne(i => i.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(i => i.UserId);

            userRole.HasOne(i => i.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(i => i.RoleId);
        });

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Tag> Tags { get; set; }
}