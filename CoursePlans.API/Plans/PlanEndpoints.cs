using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Plans.Entities;
using CoursePlans.API.Endpoints.Filters;

namespace CoursePlans.API.Plans.Endpoints;

public class PlanEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/plans")
            .WithTags("Plans")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Plans.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Course)
            .ToListAsync())
            .WithName("GetAllPlans")
            .WithSummary("Get all plans");

        group.MapPost("/", AddPlan)
            .WithSummary("Add a new Plan")
            .Produces<Plan>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithValidation<PlanDTO>();
    }

    public async Task<IResult> AddPlan(PlanDTO planDTO, CoursePlansContext db)
    {
        var course = await db.Courses.FindAsync(planDTO.CourseId);
        if (course == null) return TypedResults.BadRequest("Invalid Course");
        var plan = new Plan
        {
            Title = planDTO.Title,
            UserId = planDTO.UserId,
            CourseId = planDTO.CourseId,
            StartDate = planDTO.StartDate,
            EndDate = planDTO.EndDate
        };
        await db.Plans.AddAsync(plan);
        course.PlanCount += 1;
        db.Courses.Update(course);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/api/plans/{plan.Id}", plan);
    }
}
