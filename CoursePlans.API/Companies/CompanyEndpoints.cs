using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Companies.Entities;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;

namespace CoursePlans.API.Companies.Endpoints;

public class CompanyEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/companies")
            .WithTags("Companies")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Companies.AsNoTracking().ToListAsync())
            .WithName("GetAllCompanies")
            .WithSummary("Get all Companies");

        group.MapPost("/", async (CompanyDTO companyDTO, CoursePlansContext db) =>
        {
            var company = new Company
            {
                Name = companyDTO.Name
            };
            db.Companies.Add(company);
            await db.SaveChangesAsync();
            return Results.Created($"/api/companies/{company.Id}", company);
        });
    }
}