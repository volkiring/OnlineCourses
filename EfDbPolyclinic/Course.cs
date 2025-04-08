using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbPolyclinic
{
	public class Course
	{
		public int id { get; set; }
		public string title { get; set; }
		public string? description { get; set; }
		public DateTime? start_date { get; set; }
		public DateTime? end_date { get; set; }
		public List<Student> Students { get; set; } = new();
		public List<Teacher> Teachers { get; set; } = new();
	}
}
