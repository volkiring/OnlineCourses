using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbPolyclinic
{
	public class Grade
	{
		public int Id { get; set; }

		public int StudentId { get; set; }
		public Student Student { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }

		[Precision(3, 1)]
		[Range(0, 10)]
		public decimal NumericValue { get; set; }
	}
}
