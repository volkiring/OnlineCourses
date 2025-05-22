using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{

	public class Teacher : User
	{
		public Specialty? Specialty { get; set; }
		public List<Course> Courses { get; set; } = new();
	}
}
