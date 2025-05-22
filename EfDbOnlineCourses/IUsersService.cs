using EfDbOnlineCourses.Models;

namespace DbWebApplication
{
	public interface IUsersService
	{
		List<Course> GetUserCoursesById(string userId);

		public Student GetUserById(string userId);
	}
}