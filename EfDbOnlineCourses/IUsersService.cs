using EfDbOnlineCourses.Models;

namespace DbWebApplication
{
	public interface IUsersService
	{
		List<Course> GetUserCoursesByName(string userName);

		public User TryGetUserByName(string userName);

		public List<Course> GetTeacherCoursesByName(string userName);
	}
}