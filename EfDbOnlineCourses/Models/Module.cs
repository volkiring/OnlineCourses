using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class Module
	{
		public int Id { get; set; }	
		public string Title { get; set; }

		public Course Course { get; set; }

		public List<Lesson> Lessons { get; set; } = new();
	}
}
