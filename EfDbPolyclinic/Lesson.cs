namespace EfDbPolyclinic
{
	public class Lesson
	{
		public int id { get; set; }
		public Course course { get; set; }
		public string title { get; set; }
		public string? content { get; set; }
	}
}
