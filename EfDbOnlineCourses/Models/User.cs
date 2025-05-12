using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class User : IdentityUser
	{
		public DateTime? Birthdate { get; set; }
		public List<Course> Courses { get; set; } = new();
	}
}
