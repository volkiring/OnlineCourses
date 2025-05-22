using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ITeachersRepository
	{
		void Add(Teacher teacher, User user);
		void Delete(Teacher teacher);
		List<Teacher> GetAll();
		Teacher TryGetById(string id);
		void Update(Teacher teacher, User userModel, Teacher updatedTeacher);
	}
}