using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses
{
	public class DatabaseContext : IdentityDbContext<User>
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
			Database.Migrate();
		}
	}
}
