using Microsoft.EntityFrameworkCore;
using CoursePlans.API.Contacts.Entities;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using CoursePlans.API.Endpoints.Filters;

namespace CoursePlans.API.Contacts.Endpoints;

public class ContactEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/contacts")
            .WithTags("Contacts")
            .WithOpenApi();

        group.MapGet("/", async (CoursePlansContext db) =>
            await db.Contacts.AsNoTracking().ToListAsync())
            .WithName("GetAllContacts")
            .WithSummary("Get all Contacts");

        group.MapPost("/", AddContact)
            .WithSummary("Add a new Contact")
            .Produces<Contact>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithValidation<ContactDTO>();
    }

    public async Task<IResult> AddContact(ContactDTO contactDTO, CoursePlansContext db)
    {
        var company = await db.Companies.FindAsync(contactDTO.CompanyId);
        if (company == null) return TypedResults.BadRequest("Invalid Company");
        var contact = new Contact
        {
            Name = contactDTO.Name,
            Email = contactDTO.Email,
            CompanyId = contactDTO.CompanyId
        };
        await db.Contacts.AddAsync(contact);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/api/contacts/{contact.Id}", contact);
    }
}