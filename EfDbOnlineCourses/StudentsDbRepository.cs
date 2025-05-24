using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

		public List<User> GetAll()
		{
			return dbcontext.Students.Include(c => c.Courses).ToList();
		}
		public void Add(User student, string password)
		{
			userManager.CreateAsync(student, password).Wait();
		}

		public void Update(User student, User updatedStudent)
		{
			student.UserName = updatedStudent.UserName;
			student.Birthdate = updatedStudent.Birthdate;

			userManager.UpdateAsync(student).Wait();
		}

		public void Delete(User student)
		{
			userManager.DeleteAsync(student).Wait();
		}

		public User TryGetById(string id)
		{
			return dbcontext.Students.Include(c => c.Courses).FirstOrDefault(s => s.Id == id);
		}
	}
}
