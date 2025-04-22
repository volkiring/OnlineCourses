using EfDbOnlineCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class CoursesDbRepository : ICoursesRepository
	{
		private readonly DatabaseContext dbcontext;

		public CoursesDbRepository(DatabaseContext dbcontext)
		{
			this.dbcontext = dbcontext;
		}
		public List<Course> GetAll()
		{
			return dbcontext.Courses.ToList();
		}
		public void Add(Course course)
		{
			var newCourse = new Course()
			{
				Title = course!.Title,
				Description = course!.Description
			};
			dbcontext.Courses.Add(newCourse);
			dbcontext.SaveChanges();
		}
		public void Update(Course course, string title, string description)
		{
			course.Title = title;
			course.Description = description;
			dbcontext.SaveChanges();
		}
		public void Delete(Course course)
		{
			dbcontext.Courses.Remove(course);
			dbcontext.SaveChanges();
		}

		public Course TryGetById(int id)
		{
			return dbcontext.Courses.FirstOrDefault(c => c.Id == id);
		}
	}
}
