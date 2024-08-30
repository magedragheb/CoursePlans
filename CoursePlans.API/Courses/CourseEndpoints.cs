using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Courses.Entities;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Endpoints.Filters;

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

        group.MapPost("/", AddCourse)
            .WithSummary("Add a new Course")
            .Produces<Course>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithValidation<CourseDTO>();
    }

    public async Task<IResult> AddCourse(CourseDTO courseDTO, CoursePlansContext db)
    {
        var course = new Course
        {
            Name = courseDTO.Name,
            Code = courseDTO.Code
        };
        await db.Courses.AddAsync(course);
        await db.SaveChangesAsync();
        return Results.Created($"/api/courses/{course.Id}", course);
    }

}