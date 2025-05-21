using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
	public class Course : IEquatable<Course>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? ImagePath { get; set; }
	
		public List<Student> Students { get; set; } = new();
		public List<Teacher> Teachers { get; set; } = new();

		public bool Equals(Course? other)
		{
			if (other is null)
				return false;

			return Id == other.Id;
		}
		public override bool Equals(object? obj)
		{
			return Equals(obj as Course);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}

	
}