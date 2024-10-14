using Teppi.Application.Models;
using Teppi.Share.Entities;

namespace Teppi.Application.Interfaces;

public interface ICourseService
{
    Task<Course> CreateCourse(Course course);
    Task<Course> UpdateCourse(string id, Course course);
    Task DeleteCourse(string id);
    Task<Pagination<Course>> GetAllCourses(QueryParameters queryParam);
    Task<Result> GetCourseDetail(string id);
}