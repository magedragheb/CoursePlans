using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Users.Entities;

namespace CoursePlans.API.Users.Endpoints;

public class UserEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users")
            .WithTags("Users")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Users.AsNoTracking().ToListAsync())
            .WithName("GetAllUsers")
            .WithSummary("Get all users");

        group.MapPost("/", AddUser)
            .WithSummary("Add a new User")
            .Produces<User>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public async Task<IResult> AddUser(UserDTO userDTO, CoursePlansContext db)
    {
        var user = new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email
        };
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return Results.Created($"/api/users/{user.Id}", user);
    }
}