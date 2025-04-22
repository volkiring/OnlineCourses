using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ICoursesRepository
	{
		void Add(Course course);
		void Delete(Course course);
		List<Course> GetAll();
		Course TryGetById(int id);
		public void Update(Course course, string title, string description);
	}
}