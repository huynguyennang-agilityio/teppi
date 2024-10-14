using Microsoft.EntityFrameworkCore;
using Teppi.Application.Interfaces;
using Teppi.Application.Models;
using Teppi.Data.DbContexts;
using Teppi.Data.Persistence;
using Teppi.Share.Entities;

namespace Teppi.Application.Services;

public class CourseService : ICourseService
{
    private readonly TeppiDbContext _context;

    public CourseService(TeppiDbContext context)
    {
        _context = context;
    }
    public Task<Course> CreateCourse(Course course)
    {
    _context.Courses.Add(course);
    _context.SaveChanges();
    return Task.FromResult(course);

    }

    public Task<Course> UpdateCourse(string id, Course course)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCourse(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Pagination<Course>> GetAllCourses(QueryParameters queryParam)
    {
        List<Course> courses = _context.Courses
            .AsNoTracking()
            .ToList();
        return Task.FromResult(new Pagination<Course>(courses, courses.Count(), 1));
    }

    public Task<Result> GetCourseDetail(string id)
    {
        throw new NotImplementedException();
    }
}
