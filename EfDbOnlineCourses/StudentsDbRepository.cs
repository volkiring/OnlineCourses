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

		public List<Student> GetAll()
		{
			return dbcontext.Students.Include(u => u.User).Include(c => c.Courses).ToList();
		}
		public void Add(Student student, User user)
		{
			userManager.CreateAsync(user, student.Password).Wait();
			student.User = user;	
			dbcontext.Students.Add(student);
			dbcontext.SaveChanges();
		}

		public void Update(Student student, Student updatedStudent, User userModel)
		{
			student.User.UserName = userModel.UserName;
			student.User.PasswordHash = userManager.PasswordHasher.HashPassword(userModel, updatedStudent.Password);
			student.User.Email = userModel.Email;
			student.Name = updatedStudent.Name;
			student.Birthdate = updatedStudent.Birthdate;

			userManager.UpdateAsync(student.User).Wait();
			dbcontext.SaveChanges();
		}

		public void Delete(Student student)
		{
			var user = student.User;
			userManager.DeleteAsync(user).Wait();
			dbcontext.Students.Remove(student);
			dbcontext.SaveChanges();
		}

		public Student TryGetById(int id)
		{
			return dbcontext.Students.Include(u => u.User).Include(c => c.Courses).FirstOrDefault(c => c.Id == id);
		}
	}
}
