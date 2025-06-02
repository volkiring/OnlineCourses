using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class StudentsDbRepository : IStudentsRepository
	{
		private readonly DatabaseContext dbcontext;
		private readonly UserManager<User> userManager;

		public StudentsDbRepository(DatabaseContext dbcontext, UserManager<User> userManager)
		{
			this.dbcontext = dbcontext;
			this.userManager = userManager;
		}

		public List<Student> GetAll()
		{
			return dbcontext.Students
				.Include(s => s.User)
				.ThenInclude(u => u.Courses)
				.Include(s => s.User)
				.ThenInclude(s => s.Requests)
				.ThenInclude(r => r.Type)
			.ToList();
		}

		public IdentityResult Add(User user, string password, out User createdUser)
		{
			var result = userManager.CreateAsync(user, password).Result;
			createdUser = user;

			if (!result.Succeeded)
				return result;

			var student = new Student
			{
				UserId = user.Id,
				User = user
			};

			try
			{
				dbcontext.Students.Add(student);
				dbcontext.SaveChanges();
				return result;
			}
			catch (Exception)
			{
				userManager.DeleteAsync(user).Wait();
				throw;
			}
		}
		public void Update(Student student, Student updatedStudent)
		{
			var user = student.User;

			user.UserName = updatedStudent.User.UserName;
			user.Email = updatedStudent.User.Email;
			user.Birthdate = updatedStudent.User.Birthdate;

			userManager.UpdateAsync(user).Wait();
			dbcontext.SaveChanges();
		}

		public void Delete(Student student)
		{
			var user = student.User;

			dbcontext.Students.Remove(student);
			dbcontext.SaveChanges();

			userManager.DeleteAsync(user).Wait();
		}

		public Student TryGetById(string id)
		{
			return dbcontext.Students
				.Include(s => s.User)
				.ThenInclude(s => s.Requests)
				.ThenInclude(r => r.Type)
				.Include(s => s.User)
				.ThenInclude(u => u.Courses)
				.FirstOrDefault(s => s.UserId == id);
		}
	}

}
