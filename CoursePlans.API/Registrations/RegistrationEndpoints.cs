using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Registrations.Entities;

namespace CoursePlans.API.Registrations.Endpoints;

public class RegistrationEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/registrations")
            .WithTags("Registrations")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Registrations.AsNoTracking().ToListAsync())
            .WithName("GetAllRegistrations")
            .WithSummary("Get all registrations");

        group.MapPost("/", async (RegistrationDTO registrationDTO, CoursePlansContext db) =>
        {
            var registration = new Registration
            {
                PlanId = registrationDTO.PlanId,
                CourseId = registrationDTO.CourseId,
                UserId = registrationDTO.UserId,
                CompanyId = registrationDTO.CompanyId,
                Price = registrationDTO.Price,
                Date = registrationDTO.Date
            };
            db.Registrations.Add(registration);
            await db.SaveChangesAsync();
            return Results.Created($"/api/registrations/{registration.Id}", registration);
        });
    }
}
