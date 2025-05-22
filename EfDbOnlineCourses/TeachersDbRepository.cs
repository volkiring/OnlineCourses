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
			return dbcontext.Teachers.Include(t => t.Courses).Include(t => t.Specialty).ToList();
		}
		public void Add(Teacher teacher, string password)
		{
			userManager.CreateAsync(teacher, password).Wait();
		}

		public void Update(Teacher teacher, Teacher updatedTeacher)
		{
			teacher.UserName = updatedTeacher.UserName;
			teacher.Email = updatedTeacher.Email;
			teacher.Birthdate = updatedTeacher.Birthdate;
			teacher.Specialty = updatedTeacher.Specialty;

			userManager.UpdateAsync(teacher).Wait();
			dbcontext.SaveChanges();
		}
		public void Delete(Teacher teacher)
		{
			userManager.DeleteAsync(teacher).Wait();
		}

		public Teacher TryGetById(string id)
		{
			return dbcontext.Teachers.Include(c => c.Courses).Include(t => t.Specialty).FirstOrDefault(t => t.Id == id);
		}
	}
}
