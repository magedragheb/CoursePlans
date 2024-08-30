namespace CoursePlans.API.Companies.Entities;

public class Company
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public record CompanyDTO(string Name);