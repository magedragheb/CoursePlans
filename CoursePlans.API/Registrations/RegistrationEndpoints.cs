using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Registrations.Entities;
using CoursePlans.API.Endpoints.Filters;

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

        group.MapPost("/", Register)
            .WithSummary("Add a new registration")
            .Produces<Registration>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithValidation<RegistrationDTO>();
    }

    public async Task<IResult> Register(RegistrationDTO registrationDTO, CoursePlansContext db)
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
        await db.Registrations.AddAsync(registration);
        await db.SaveChangesAsync();
        return Results.Created($"/api/registrations/{registration.Id}", registration);
    }
}
