using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbPolyclinic
{
	public class Teacher
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string? Specialty { get; set; }

		public List<Course> Courses { get; set; } = new();
	}
}
