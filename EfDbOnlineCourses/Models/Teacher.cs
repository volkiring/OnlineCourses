using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{

	public class Teacher
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Specialty is required.")]
		public string Specialty { get; set; }

		public DateTime? Birthdate { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
		public string Password { get; set; }

		public List<Course> Courses = new();

		public User User { get; set; }
	}



}
