using Microsoft.EntityFrameworkCore;
using Teppi.Share.Entities;

namespace Teppi.Data.DbContexts;

public class CategoryContext: DbContext
{
    public CategoryContext(DbContextOptions<CategoryContext> options)
        : base(options) { }

    public DbSet<Category> Categories => Set<Category>();

}