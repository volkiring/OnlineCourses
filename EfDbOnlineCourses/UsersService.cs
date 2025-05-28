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
			var user = TryGetUserById(userId);

			return user?.Courses ?? new List<Course>();
		}

		public User TryGetUserById(string userId)
		{
			return databaseContext.Users.Include(u => u.Courses).FirstOrDefault(u => u.Id == userId);
		}

	}
}
