using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class Teacher : Student
	{
		public string? Specialty { get; set; }
	}
}
