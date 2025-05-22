using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IStudentsRepository
	{
		void Add(Student student, User user);
		void Delete(Student student);
		List<Student> GetAll();
		Student TryGetById(string id);
		void Update(Student student, Student updatedStudent, User user);
	}
}