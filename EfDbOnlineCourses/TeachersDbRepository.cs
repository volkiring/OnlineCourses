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
			return dbcontext.Teachers.Include(u => u.User).Include(c => c.Courses).ToList();
		}
		public void Add(Teacher teacher, User user)
		{
			userManager.CreateAsync(user, teacher.Password).Wait();
			teacher.User = user;
			dbcontext.Teachers.Add(teacher);
			dbcontext.SaveChanges();
		}

		public void Update(Teacher teacher, User userModel, Teacher updatedTeacher)
		{
			teacher.User.UserName = userModel.UserName;
			teacher.User.PasswordHash = userManager.PasswordHasher.HashPassword(userModel, updatedTeacher.Password);
			teacher.User.Email = userModel.Email;
			teacher.Name = updatedTeacher.Name;
			teacher.Birthdate = updatedTeacher.Birthdate;
			teacher.Specialty = updatedTeacher.Specialty;

			userManager.UpdateAsync(teacher.User).Wait();
			dbcontext.SaveChanges();
		}
		public void Delete(Teacher teacher)
		{
			var user = teacher.User;
			userManager.DeleteAsync(user).Wait();
			dbcontext.Teachers.Remove(teacher);
			dbcontext.SaveChanges();
		}

		public Teacher TryGetById(int id)
		{
			return dbcontext.Teachers.Include(u => u.User).Include(c => c.Courses).FirstOrDefault(c => c.Id == id);
		}
	}
}
