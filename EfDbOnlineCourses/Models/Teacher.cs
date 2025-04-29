using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class Teacher 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Email { get; set; }
		public DateTime? Birthdate { get; set; }
		public List<Course> Courses { get; set; } = new();
		public string? Specialty { get; set; }
	}
}
