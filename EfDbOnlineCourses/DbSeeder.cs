using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;

public static class DbSeeder
{
	public static void SeedDatabase(DatabaseContext context)
	{
		if (context.Courses.Any()) return; 

		var specialty = new Specialty { Name = "Информатика" };
		context.Specialties.Add(specialty);

		var course = new Course
		{
			Title = "Основы C#",
			Description = "Изучение основ языка программирования C#",
			StartDate = DateTime.Today,
			EndDate = DateTime.Today.AddMonths(3),
			ImagePath = "/images/courses/csharp-course.png"
		};

		var module1 = new Module
		{
			Title = "Введение в C#",
			Course = course,
			Lessons = new List<Lesson>
			{
				new Lesson { Title = "История языка", Content = "C# был создан Microsoft в 2000 году..." },
				new Lesson { Title = "Hello World", Content = "using System; class Program { static void Main() => Console.WriteLine(\"Hello World\"); }" }
			}
		};

		var module2 = new Module
		{
			Title = "ООП в C#",
			Course = course,
			Lessons = new List<Lesson>
			{
				new Lesson { Title = "Классы и Объекты", Content = "C# поддерживает классы и объекты..." },
				new Lesson { Title = "Наследование", Content = "Один класс может наследовать другой..." }
			}
		};

		course.Modules = new List<Module> { module1, module2 };
		context.Courses.Add(course);
		context.SaveChanges();
	}
}
