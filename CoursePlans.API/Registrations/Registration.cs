using CoursePlans.API.Companies.Entities;
using CoursePlans.API.Courses.Entities;
using CoursePlans.API.Plans.Entities;
using CoursePlans.API.Trainees.Entities;
using CoursePlans.API.Users.Entities;

namespace CoursePlans.API.Registrations.Entities;

public class Registration
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public decimal Price { get; set; }
    public DateOnly Date { get; set; }
    public Plan? Plan { get; set; }
    public Course? Course { get; set; }
    public User? User { get; set; }
    public Company? Company { get; set; }
    public ICollection<Trainee>? Trainees { get; set; }
}

public record RegistrationDTO(int PlanId, int CourseId, int UserId, int CompanyId, decimal Price, DateOnly Date);