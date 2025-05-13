using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;

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
			return dbcontext.Teachers.ToList();
		}
		public void Add(Teacher teacher)
		{
			var user = teacher.User;
			userManager.CreateAsync(user).Wait();

			dbcontext.Teachers.Add(teacher);
			dbcontext.SaveChanges();
		}

		public void Update(Teacher teacher, Teacher updatedTeacher)
		{
			var user = teacher.User;
			user.PasswordHash = userManager.PasswordHasher.HashPassword(user, updatedTeacher.Password);
			user.UserName = updatedTeacher.User.UserName;
			teacher.User.Email = updatedTeacher.User.Email;
			userManager.UpdateAsync(user).Wait();

			teacher.Name = updatedTeacher.Name;
			teacher.Specialty = updatedTeacher.Specialty;
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
			return dbcontext.Teachers.FirstOrDefault(c => c.Id == id);
		}
	}
}
