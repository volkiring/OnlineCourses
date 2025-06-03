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

		public List<Course> GetUserCoursesByName(string userName)
		{
			var user = TryGetUserByName(userName);

			return user?.Courses ?? new List<Course>();
		}

		public User TryGetUserByName(string userName)
		{
			return databaseContext.Users.Include(u => u.Courses).Include(r => r.Requests).ThenInclude(r => r.Type).Include(r => r.Requests).ThenInclude(r => r.Specialty).Include(u => u.Teacher).Include(u => u.Student).FirstOrDefault(u => u.UserName == userName);
		}

		public List<Course> GetTeacherCoursesByName(string userName)
		{
			var user = TryGetUserByName(userName);

			return user?.Teacher.CoursesTaught ?? new List<Course>();
		}


	}
}
