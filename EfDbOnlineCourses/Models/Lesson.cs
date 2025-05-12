using Microsoft.AspNetCore.Identity;

namespace EfDbOnlineCourses.Models
{
	public class Lesson : IdentityUser
	{
		public int Id { get; set; }
		public Course Course { get; set; }

		public string Title { get; set; }
		public string? Content { get; set; }
	}
}
