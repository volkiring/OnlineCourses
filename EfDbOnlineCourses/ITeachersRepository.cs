using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ITeachersRepository
	{
		void Add(Teacher teacher);
		void Delete(Teacher teacher);
		List<Teacher> GetAll();
		Teacher TryGetById(int id);
		void Update(Teacher teacher, Teacher updatedTeacher);
	}
}