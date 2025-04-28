using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class TeachersDbRepository : ITeachersRepository
	{
		private readonly DatabaseContext dbcontext;
		public TeachersDbRepository(DatabaseContext dbContext)
		{
			this.dbcontext = dbContext;
		}

		public List<Teacher> GetAll()
		{
			return dbcontext.Teachers.ToList();
		}
		public void Add(Teacher teacher)
		{
			dbcontext.Teachers.Add(teacher);
			dbcontext.SaveChanges();
		}
		public void Update(Teacher teacher, Teacher updatedTeacher)
		{
			teacher.Name = updatedTeacher.Name;
			teacher.Email = updatedTeacher.Email;
			teacher.Specialty = updatedTeacher.Specialty;
			dbcontext.SaveChanges();
		}
		public void Delete(Teacher teacher)
		{
			dbcontext.Teachers.Remove(teacher);
			dbcontext.SaveChanges();
		}

		public Teacher TryGetById(int id)
		{
			return dbcontext.Teachers.FirstOrDefault(c => c.Id == id);
		}
	}
}
