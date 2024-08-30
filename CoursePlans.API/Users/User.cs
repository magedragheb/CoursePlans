namespace CoursePlans.API.Users.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}

public record UserDTO(string Name, string Email);