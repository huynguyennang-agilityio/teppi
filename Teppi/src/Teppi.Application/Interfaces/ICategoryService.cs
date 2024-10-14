using Teppi.Application.Models;
using Teppi.Share.Entities;

namespace Teppi.Application.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateCategory(Category category);
    Task<Category> UpdateCategory(string id, Category category);
    Task<Boolean> DeleteCategory(string id);
    Task<Pagination<Category>> GetAllCategories(QueryParameters queryParam);
    Task<Result> GetCategoryDetail(string id);
}