using Teppi.Share.Entities;

namespace Teppi.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

public class CourseContext: DbContext
{
    public CourseContext(DbContextOptions<CourseContext> options)
        : base(options) { }

    public DbSet<Course> Courses => Set<Course>();

}