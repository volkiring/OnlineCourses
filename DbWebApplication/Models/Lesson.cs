namespace EfDbOnlineCourses
{
	public class Lesson
	{
		public int Id { get; set; }
		public CourseViewModel Course { get; set; }

		public string Title { get; set; }
		public string? Content { get; set; }
	}
}
