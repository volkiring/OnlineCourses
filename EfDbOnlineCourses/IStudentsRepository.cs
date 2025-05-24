using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IStudentsRepository
	{
		void Add(User student, string password);
		void Delete(User student);
		List<User> GetAll();
		User TryGetById(string id);
		void Update(User student, User updatedStudent);
	}
}