using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;

namespace EfDbOnlineCourses
{
	public interface IStudentsRepository
	{
		IdentityResult Add(User user, string password, out User createdUser);
		void Delete(Student student);
		List<Student> GetAll();
		Student TryGetById(string id);
		Student TryGetByUserName(string userName);
		void Update(Student student, Student updatedStudent);
	}
}