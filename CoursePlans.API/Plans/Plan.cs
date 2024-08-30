using CoursePlans.API.Courses.Entities;
using CoursePlans.API.Users.Entities;

namespace CoursePlans.API.Plans.Entities;

public class Plan
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public User? User { get; set; }
    public Course? Course { get; set; }
}

public record PlanDTO(string Title, int UserId, int CourseId, DateOnly StartDate, DateOnly EndDate);