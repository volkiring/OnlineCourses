using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IStudentsRepository
	{
		void Add(Student student);
		void Delete(Student student);
		List<Student> GetAll();
		Student TryGetById(int id);
		void Update(Student student, Student updatedStudent);
	}
}