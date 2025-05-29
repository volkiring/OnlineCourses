using EfDbOnlineCourses.Models;

namespace DbWebApplication
{
	public interface IUsersService
	{
		List<Course> GetUserCoursesById(string userName);

		public User TryGetUserByName(string userName);
	}
}