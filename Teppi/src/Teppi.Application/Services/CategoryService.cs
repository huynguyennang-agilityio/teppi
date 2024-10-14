using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Teppi.Application.Interfaces;
using Teppi.Application.Models;
using Teppi.Data.Persistence;
using Teppi.Share.Entities;
using Teppi.WebApi.Extensions;

namespace Teppi.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly TeppiDbContext _context;
    private readonly IDistributedCache _cache;

    public CategoryService(TeppiDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public Task<Category> CreateCategory(Category newCategory)
    {
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        var cacheKey = "Categories";
        _cache.Remove(cacheKey);

        return Task.FromResult(newCategory);
    }

    public Task<Category> UpdateCategory(string id, Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Boolean> DeleteCategory(string id)
    {
        // TODO: Implement delete category
        return (int.Parse(id) % 2 == 0) ? Task.FromResult(true) : Task.FromResult(false);
    }

    public async Task<Pagination<Category>> GetAllCategories(QueryParameters queryParam)
    {
        var cacheKey = "Categories";
        var cacheOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(20))
            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        var categories = await _cache.GetOrSetAsync(
            cacheKey,
            async () =>
            {
                return _context.Categories
                    .AsNoTracking()
                    .ToList();
            },
            cacheOptions);
        return await Task.FromResult(new Pagination<Category>(
            categories ?? [],
            (categories ?? []).Count(),
            1));
    }

    public async Task<Result> GetCategoryDetail(string id)
    {
        var cacheKey = $"category:{id}";
        var category = await _cache.GetOrSetAsync(cacheKey,
            async () => _context.Categories.FromSqlRaw("SELECT * FROM Categories WHERE Id = {0}", id)
                .AsEnumerable()
                .FirstOrDefault())!;
        return Result.Success(category);
    }
}