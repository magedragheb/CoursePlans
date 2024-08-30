using CoursePlans.API.Companies.Entities;
using CoursePlans.API.Contacts.Entities;
using CoursePlans.API.Registrations.Entities;

namespace CoursePlans.API.Trainees.Entities;

public class Trainee
{
    public int Id { get; set; }
    public int RegistrationId { get; set; }
    public int CompanyId { get; set; }
    public int ContactId { get; set; }
    public Registration? Registration { get; set; } 
    public Company? Company { get; set; }
    public Contact? Contact { get; set; } 
}

public record TraineeDTO (int RegistrationId, int CompanyId, int ContactId);