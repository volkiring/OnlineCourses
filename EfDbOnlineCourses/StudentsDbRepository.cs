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
			return dbcontext.Students.Include(c => c.Courses).ToList();
		}
		public void Add(Student studentViewModel, User user)
		{
			userManager.CreateAsync(user, studentViewModel.Password).Wait();
			dbcontext.Students.Add(studentViewModel);
			dbcontext.SaveChanges();
		}

		public void Update(Student student, Student updatedStudent, User userModel)
		{
			student.UserName = userModel.UserName;
			student.PasswordHash = userManager.PasswordHasher.HashPassword(userModel, updatedStudent.Password);
			student.Email = userModel.Email;
			student.Birthdate = updatedStudent.Birthdate;

			userManager.UpdateAsync(student).Wait();
			dbcontext.SaveChanges();
		}

		public void Delete(Student student)
		{
			userManager.DeleteAsync(student).Wait();
			dbcontext.Students.Remove(student);
			dbcontext.SaveChanges();
		}

		public Student TryGetById(string id)
		{
			return dbcontext.Students.Include(c => c.Courses).FirstOrDefault(s => s.Id == id);
		}
	}
}
