using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses
{
	public class DatabaseContext : IdentityDbContext<User>
	{
		public DbSet<User> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Grade> Grades { get; set; }

		public DbSet<Specialty> Specialties { get; set; }
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Student>().ToTable("Students").HasBaseType<User>();
			modelBuilder.Entity<Teacher>().ToTable("Teachers").HasBaseType<User>();

			modelBuilder.Entity<Course>()
				.HasMany(c => c.Users)
				.WithMany(u => u.Courses)
				.UsingEntity(j => j.ToTable("CoursesUser")); 
		}

	}
}
