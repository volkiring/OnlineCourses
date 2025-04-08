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
		public int id { get; set; }
		public Student student { get; set; }
		public Course course { get; set; }

		[Precision(3, 1)]
		[Range(0, 10)]
		public decimal numeric_value { get; set; }
	}
}
