﻿using Microsoft.EntityFrameworkCore;
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
	
		public List<User> Users { get; set; }

		public override bool Equals(object? obj)
		{
			return Equals(obj as Course);
		}

		public bool Equals(Course? other)
		{
			return other != null && this.Id == other.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

	}


}