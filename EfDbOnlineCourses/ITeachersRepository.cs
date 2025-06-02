using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ITeachersRepository
	{
		bool Add(User user, Specialty specialty);
		void Delete(Teacher teacher);
		List<Teacher> GetAll();
		Teacher TryGetById(string id);
		void Update(Teacher teacher, Teacher updatedTeacher);
	}
}