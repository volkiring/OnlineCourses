using EfDbOnlineCourses.Models;
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
		public DbSet<Request> Requests { get; set; }
		public DbSet<RequestType> RequestTypes { get; set; }

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

			modelBuilder.Entity<Teacher>()
			.HasMany(t => t.CoursesTaught)
			.WithMany(c => c.Teachers)
			.UsingEntity(j => j.ToTable("CourseTeachers"));

			modelBuilder.Entity<RequestType>().HasData(
			new RequestType()
			{
				Id = 1,
				Name = "Заявка на становление преподавателем"
			}
			);

			modelBuilder.Entity<Specialty>().HasData(
			new Specialty { Id = 1, Name = "Математика" },
			new Specialty { Id = 2, Name = "Программирование" }
			);

			modelBuilder.Entity<Request>()
	.Property(r => r.Status)
	.HasConversion<string>();

		}

	}
}
