using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Trainees.Entities;

namespace CoursePlans.API.Trainees.Endpoints;

public class TraineeEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/trainees")
            .WithTags("Trainees")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Trainees.AsNoTracking().ToListAsync())
            .WithName("GetAllTrainees")
            .WithSummary("Get all trainees");

        group.MapPost("/", AddTrainee)
            .WithSummary("Add a new trainee")
            .Produces<Trainee>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public async Task<IResult> AddTrainee(TraineeDTO traineeDTO, CoursePlansContext db)
    {
        var trainee = new Trainee
        {
            RegistrationId = traineeDTO.RegistrationId,
            CompanyId = traineeDTO.CompanyId,
            ContactId = traineeDTO.ContactId
        };
        await db.Trainees.AddAsync(trainee);
        await db.SaveChangesAsync();
        return Results.Created($"/api/trainees/{trainee.Id}", trainee);
    }
}