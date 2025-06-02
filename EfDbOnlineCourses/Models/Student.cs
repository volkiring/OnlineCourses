using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EfDbOnlineCourses.Models
{
	public class Student
	{
		[Key]
		[ForeignKey("User")]
		public string UserId { get; set; } = null!;

		public User User { get; set; } = null!;
	}

}
