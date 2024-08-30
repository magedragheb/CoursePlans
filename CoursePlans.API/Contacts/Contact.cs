using CoursePlans.API.Companies.Entities;

namespace CoursePlans.API.Contacts.Entities;

public class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int CompanyId { get; set; }
    public Company? Company { get; set; }
}

public record ContactDTO(string Name, string Email, int CompanyId);