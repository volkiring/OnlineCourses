using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbPolyclinic
{
	public class Teacher
	{
		public int id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string? speciality { get; set; }
		public List<Course> courses { get; set; } = new();
	}
}
