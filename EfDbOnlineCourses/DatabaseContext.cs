using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext 
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Lesson> Lessons { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Grade> Grades { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
	{ 
		Database.EnsureCreated();
		Database.Migrate();
	}
}
