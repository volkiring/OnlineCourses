using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbPolyclinic
{
	public class Student
	{
		public int id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public DateTime? birthdate { get; set; }
		public List<Course> Courses { get; set; } = new();
	}
}
