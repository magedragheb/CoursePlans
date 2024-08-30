namespace CoursePlans.API.Courses.Entities;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public int PlanCount { get; set; } = 0;
}

public record CourseDTO(string Name, string Code);