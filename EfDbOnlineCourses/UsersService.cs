using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace DbWebApplication
{
	public class UsersService : IUsersService
	{
		private readonly DatabaseContext databaseContext;

		public UsersService(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public List<Course> GetUserCoursesById(string userId)
		{
			var student = GetStudentById(userId);

			return student?.Courses ?? new List<Course>();
		}

		public Student GetStudentById(string userId)
		{
			return databaseContext.Students.Include(s => s.Courses).FirstOrDefault(s => s.User.Id == userId);
		}
	}
}
