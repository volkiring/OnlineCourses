using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
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
			return dbcontext.Courses.Include(s => s.Students).Include(t => t.Teachers).ThenInclude(t => t.Specialty).ToList();
		}
		public void Add(Course course)
		{
			dbcontext.Courses.Add(course);
			dbcontext.SaveChanges();
		}
		public void Update(Course course, Course updatedCourse, string? imagePath)
		{
			course.Title = updatedCourse.Title;
			course.Description = updatedCourse.Description;
			course.StartDate = updatedCourse.StartDate;
			course.EndDate = updatedCourse.EndDate;
			course.ImagePath = imagePath ?? course.ImagePath;
			dbcontext.SaveChanges();
		}
		public void Delete(Course course)
		{
			dbcontext.Courses.Remove(course);
			dbcontext.SaveChanges();
		}

		public Course TryGetById(int id)
		{
			return dbcontext.Courses
				.Include(c => c.Teachers)
				.ThenInclude(t => t.Specialty)
				.Include(c => c.Students)
				.FirstOrDefault(c => c.Id == id);
		}


		public void AddTeacherToCourse(Course course, Teacher teacher)
		{
			course.Teachers.AddRange(teacher);
			dbcontext.SaveChanges();
		}

		public void AddStudentToCourse(Course course, Student student)
		{
			course.Students.AddRange(student);
			dbcontext.SaveChanges();
		}

		public void DeleteTeacherToCourse(Course course, Teacher teacher)
		{
			course.Teachers.Remove(teacher);
			dbcontext.SaveChanges();
		}

		public void DeleteStudentToCourse(Course course, Student student)
		{
			course.Students.Remove(student);
			dbcontext.SaveChanges();
		}

	}
}
