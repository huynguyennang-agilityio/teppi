using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Teppi.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Authorize]
public class CategoriesController(
    ICategoryService categoryService,
    ILogger<CategoriesController> logger
) : BaseApiController
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all categories",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Pagination<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pagination<Course>>> Get([FromQuery] QueryParameters queryParam)
    {
        logger.LogInformation("Get all courses");
        var categories = await categoryService.GetAllCategories(queryParam);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Find a particular category by ID",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> Get(string id)
    {
        logger.LogInformation("Get category by id: {Id}", id);
        var category = await categoryService.GetCategoryDetail(id);
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    [SwaggerOperation(
        Summary = "Add a new course",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Category>> Post(Category category)
    {
        var newCategory = await categoryService.CreateCategory(category);
        return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    [SwaggerOperation(
        Summary = "Update a category",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> Put(string id, Category category)
    {
        logger.LogInformation("Update a category: {Id}", id);
        var updatedcCategory = await categoryService.UpdateCategory(id, category);
        return Ok(updatedcCategory);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    [SwaggerOperation(
        Summary = "Delete a category",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Boolean> Delete(string id)
    {
        logger.LogInformation("Delete category: {Id}", id);
        Boolean isDelete = await categoryService.DeleteCategory(id);
        return isDelete;
    }
}