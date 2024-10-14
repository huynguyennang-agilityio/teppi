using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Teppi.WebApi.Extensions;

namespace Teppi.WebApi.Controllers.V1;

[ApiVersion("1.0")]
public class CoursesController(
    ICourseService courseService,
    ILogger<CoursesController> logger
) : BaseApiController
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all courses",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Pagination<Course>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pagination<Course>>> Get([FromQuery] QueryParameters queryParam)
    {
        logger.LogInformation("Get all courses");
        var courses = await courseService.GetAllCourses(queryParam);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Find a particular course by ID",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        logger.LogInformation("Get course by id: {Id}", id);
        var course = await courseService.GetCourseDetail(id);
 
        return course.IsSuccess ? Ok(course) : course.ToProblemDetails();
    }

    [HttpPost]
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
    public async Task<ActionResult<Course>> Post(Course course)
    {
        logger.LogInformation("Add course: {Name}", course.Name);
        var newCourse = await courseService.CreateCourse(course);
        return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a course",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Course>> Put(string id, Course course)
    {
        logger.LogInformation("Update course: {Id}", id);
        var updatedCourse = await courseService.UpdateCourse(id, course);
        return Ok(updatedCourse);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a course",
        Description = "Authorize is required"
    )]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        logger.LogInformation("Delete course: {Id}", id);
        await courseService.DeleteCourse(id);
        return NoContent();
    }
}