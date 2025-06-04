using Microsoft.AspNetCore.Identity;

namespace EfDbOnlineCourses.Models
{
	public class Lesson 
	{
		public int Id { get; set; }
		public Module Module { get; set; }

		public string Title { get; set; }
		public string? Content { get; set; }
	}
}
