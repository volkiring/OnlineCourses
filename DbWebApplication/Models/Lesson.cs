using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public class Lesson
	{
		public int Id { get; set; }
		public Course Course { get; set; }

		public string Title { get; set; }
		public string? Content { get; set; }
	}
}
