using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ICoursesRepository
	{
		void Add(Course course);
		void AddStudentToCourse(Course course, Student student);
		void AddTeacherToCourse(Course course, Teacher teacher);
		void Delete(Course course);
		void DeleteStudentToCourse(Course course, Student student);
		void DeleteTeacherToCourse(Course course, Teacher teacher);
		List<Course> GetAll();
		Course TryGetById(int id);
		void Update(Course course, Course updatedCourse, string? imagePath);
	}
}