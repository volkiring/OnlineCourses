using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses
{
	public class TeachersDbRepository : ITeachersRepository
	{
		private readonly DatabaseContext dbcontext;
		private readonly UserManager<User> userManager;
		public TeachersDbRepository(DatabaseContext dbContext, UserManager<User> userManager)
		{
			this.dbcontext = dbContext;
			this.userManager = userManager;
		}

		public List<Teacher> GetAll()
		{
			return dbcontext.Teachers.Include(c => c.Courses).ToList();
		}
		public void Add(Teacher teacherViewModel)
		{
			var teacher = new Teacher()
			{
               UserName = teacherViewModel.UserName,
               Email = teacherViewModel.Email,
               Birthdate = teacherViewModel.Birthdate,
               Specialty = teacherViewModel.Specialty
            };
			userManager.CreateAsync(teacher, teacherViewModel.Password).Wait();
			dbcontext.Teachers.Add(teacherViewModel);
			dbcontext.SaveChanges();
		}

		public void Update(Teacher teacher, Teacher updatedTeacher)
		{
			teacher.UserName = updatedTeacher.UserName;
			teacher.PasswordHash = userManager.PasswordHasher.HashPassword(updatedTeacher, updatedTeacher.Password);
			teacher.Email = updatedTeacher.Email;
			teacher.Birthdate = updatedTeacher.Birthdate;
			teacher.Specialty = updatedTeacher.Specialty;

			userManager.UpdateAsync(teacher).Wait();
			dbcontext.SaveChanges();
		}
		public void Delete(Teacher teacher)
		{
			userManager.DeleteAsync(teacher).Wait();
			dbcontext.Teachers.Remove(teacher);
			dbcontext.SaveChanges();
		}

		public Teacher TryGetById(string id)
		{
			return dbcontext.Teachers.Include(c => c.Courses).FirstOrDefault(t => t.Id == id);
		}
	}
}
