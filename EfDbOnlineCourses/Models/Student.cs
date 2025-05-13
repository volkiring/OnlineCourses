using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class Student
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public User User { get; set; }
		public DateTime? Birthdate { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
		public string Password { get; set; }

		public List<Course> Courses = new();
	}
}
