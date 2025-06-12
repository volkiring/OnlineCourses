using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses.Repositories
{
	public class TeachersDbRepository : ITeachersRepository
	{
		private readonly DatabaseContext dbcontext;
		private readonly UserManager<User> userManager;

		public TeachersDbRepository(DatabaseContext dbContext, UserManager<User> userManager)
		{
			dbcontext = dbContext;
			this.userManager = userManager;
		}

		public List<Teacher> GetAll()
		{
			return dbcontext.Teachers
				.Include(t => t.User)
					.ThenInclude(t => t.Courses)
				.Include(t => t.User)
					.ThenInclude(t => t.Requests)
						.ThenInclude(r => r.Type)
				.Include(t => t.CoursesTaught)
				.Include(t => t.Specialty)
				.ToList();
		}

		public bool Add(User user, Specialty specialty)
		{
			if (dbcontext.Teachers.Any(t => t.UserId == user.Id))
				return false;

			var teacher = new Teacher
			{
				UserId = user.Id,
				Specialty = specialty
			};

			dbcontext.Teachers.Add(teacher);
			dbcontext.SaveChanges();
			return true;
		}


		public void Update(Teacher teacher, Teacher updatedTeacher)
		{
			var user = teacher.User;

			user.UserName = updatedTeacher.User.UserName;
			user.Email = updatedTeacher.User.Email;
			user.Birthdate = updatedTeacher.User.Birthdate;

			teacher.Specialty = updatedTeacher.Specialty;

			userManager.UpdateAsync(user).Wait();
			dbcontext.SaveChanges();
		}

		public void Delete(Teacher teacher)
		{
			var user = teacher.User;

			dbcontext.Teachers.Remove(teacher);
			dbcontext.SaveChanges();

			userManager.UpdateSecurityStampAsync(user).Wait();
			userManager.DeleteAsync(user).Wait();
		}

		public Teacher TryGetById(string id)
		{
			return dbcontext.Teachers
				.Include(t => t.User)
					.ThenInclude(t => t.Requests)
						.ThenInclude(r => r.Type)
				.Include(t => t.CoursesTaught)
				.Include(t => t.User)
					.ThenInclude(t => t.Courses)
				.Include(t => t.Specialty)
				.FirstOrDefault(t => t.UserId == id);
		}
	}

}

