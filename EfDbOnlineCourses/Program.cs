using EfDbPolyclinic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;

public class ApplicationDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Lesson> Lessons { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Grade> Grades { get; set; }

	public ApplicationDbContext()
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = "Server=localhost;Database=onlinecourses;Uid=root;Pwd=99977788866aaaAa;";
		optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var teacher1 = new Teacher { Id = 1, Name = "Alice Smith", Email = "alice@example.com", Specialty = "Math" };
		var teacher2 = new Teacher { Id = 2, Name = "Bob Johnson", Email = "bob@example.com", Specialty = "Physics" };

		var student1 = new Student { Id = 1, Name = "John Doe", Email = "john@example.com", Birthdate = new DateTime(2000, 5, 1) };
		var student2 = new Student { Id = 2, Name = "Jane Roe", Email = "jane@example.com", Birthdate = new DateTime(1999, 10, 10) };

		var course1 = new Course { Id = 1, Title = "Algebra I", Description = "Basic algebra course", StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(3) };
		var course2 = new Course { Id = 2, Title = "Physics I", Description = "Basic physics course", StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(3) };


		var lesson1 = new Lesson { Id = 1, Title = "Intro to Algebra", Content = "Numbers and operations", CourseId = 1 };
		var lesson2 = new Lesson { Id = 2, Title = "Newton's Laws", Content = "Basics of motion", CourseId = 2 };


		var grade1 = new Grade { Id = 1, StudentId = 1, CourseId = 1, NumericValue = 8.5M };
		var grade2 = new Grade { Id = 2, StudentId = 2, CourseId = 2, NumericValue = 9.2M };


		modelBuilder.Entity<Teacher>().HasData(teacher1, teacher2);
		modelBuilder.Entity<Student>().HasData(student1, student2);
		modelBuilder.Entity<Course>().HasData(course1, course2);
		modelBuilder.Entity<Lesson>().HasData(lesson1, lesson2);
		modelBuilder.Entity<Grade>().HasData(grade1, grade2);

		modelBuilder.Entity("CourseStudent").HasData(
			new { CoursesId = 1, StudentsId = 1 },
			new { CoursesId = 2, StudentsId = 2 },
			new { CoursesId = 2, StudentsId = 1 }
		);

		modelBuilder.Entity("CourseTeacher").HasData(
			new { CoursesId = 1, TeachersId = 1 },
			new { CoursesId = 2, TeachersId = 2 }
		);
	}
}



public class Program
{
	public static void Main()
	{
		using ApplicationDbContext context = new();

		var user = context.Students!.Include(c => c.Courses).FirstOrDefault(s => s.Name == "John Doe");

		Console.WriteLine(string.Join("\n", user.Courses.Select(c => $"{c.Title}, {c.Description}")));
	}
}