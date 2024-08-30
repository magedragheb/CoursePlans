using CoursePlans.API.Companies.Entities;
using CoursePlans.API.Contacts.Entities;
using CoursePlans.API.Courses.Entities;
using CoursePlans.API.Plans.Entities;
using CoursePlans.API.Registrations.Entities;
using CoursePlans.API.Trainees.Entities;
using CoursePlans.API.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursePlans.API.Data;

public class CoursePlansContext(DbContextOptions<CoursePlansContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Trainee> Trainees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("v1");

        modelBuilder.Entity<Registration>().Property(p => p.Price).HasPrecision(18, 4);
        modelBuilder.Entity<Registration>().Property(p => p.Date).HasPrecision(0);
        modelBuilder.Entity<Plan>().Property(p => p.StartDate).HasPrecision(0);
        modelBuilder.Entity<Plan>().Property(p => p.EndDate).HasPrecision(0);

    }
}