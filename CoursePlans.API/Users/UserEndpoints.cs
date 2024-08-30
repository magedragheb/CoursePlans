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
            await db.Courses.AsNoTracking().ToListAsync())
            .WithName("GetAllUsers")
            .WithSummary("Get all users");

        group.MapPost("/", async (UserDTO userDTO, CoursePlansContext db) =>
        {
            var user = new User
            {
                Name = userDTO.Name,
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/api/users/{user.Id}", user);
        });
    }
}