using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Courses.Entities;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;

namespace CoursePlans.API.Courses.Endpoints;

public class CourseEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/courses")
            .WithTags("Courses")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Courses.AsNoTracking().ToListAsync())
            .WithName("GetAllCourses")
            .WithSummary("Get all courses");

        group.MapPost("/", async (CourseDTO courseDTO, CoursePlansContext db) =>
        {
            var course = new Course
            {
                Name = courseDTO.Name
            };
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return Results.Created($"/api/courses/{course.Id}", course);
        });

    }
}