using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Plans.Entities;

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

        group.MapPost("/", async (PlanDTO planDTO, CoursePlansContext db) =>
        {
            var plan = new Plan
            {
                Title = planDTO.Title,
                UserId = planDTO.UserId,
                CourseId = planDTO.CourseId,
                StartDate = planDTO.StartDate,
                EndDate = planDTO.EndDate
            };
            db.Plans.Add(plan);
            await db.SaveChangesAsync();
            return Results.Created($"/api/plans/{plan.Id}", plan);
        });
    }
}
