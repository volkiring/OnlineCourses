using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EfDbOnlineCourses.Models
{

	public class Teacher
	{
		[Key]
		[ForeignKey("User")]
		public string UserId { get; set; } = null!;

		public User User { get; set; } = null!;

		public Specialty? Specialty { get; set; }

		public List<Course> CoursesTaught { get; set; } = new();
	}

}
