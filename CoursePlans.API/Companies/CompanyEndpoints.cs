using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Companies.Entities;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Endpoints.Filters;

namespace CoursePlans.API.Companies.Endpoints;

public class CompanyEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/companies")
            .WithTags("Companies")
            .WithOpenApi();

        group.MapGet("/", GetCompanies)
            .WithName("GetAllCompanies")
            .WithSummary("Get all Companies");

        group.MapPost("/", AddCompany)
            .WithName("AddCompany")
            .WithSummary("Add a new Company")
            .Produces<Company>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithValidation<Company>();
    }

    public async Task<IResult> GetCompanies(CoursePlansContext db) =>
        Results.Ok(await db.Companies.AsNoTracking().ToListAsync());

    public async Task<IResult> AddCompany(CompanyDTO companyDTO, CoursePlansContext db)
    {
        var company = new Company
        {
            Name = companyDTO.Name
        };
        db.Companies.Add(company);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/api/companies/{company.Id}", company);
    }
}