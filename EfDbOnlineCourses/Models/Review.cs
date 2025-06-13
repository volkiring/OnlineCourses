using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class Review
	{
		public int Id { get; set; }

		[Required]
		public string Content { get; set; }

		[Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
		public int Rating { get; set; }

		public DateTime DatePosted { get; set; } = DateTime.Now;
		public Course Course { get; set; }
		public Student Student { get; set; }
	}
}
