using EfDbOnlineCourses.Models;
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

		public StudentsDbRepository(DatabaseContext dbcontext)
		{
			this.dbcontext = dbcontext;
		}

		public List<Student> GetAll()
		{
			return dbcontext.Students.ToList();
		}
		public void Add(Student student)
		{
			dbcontext.Students.Add(student);
			dbcontext.SaveChanges();
		}
		public void Update(Student student, Student updatedStudent)
		{
			student.Name = updatedStudent.Name;
			student.Email = updatedStudent.Email;
			student.Birthdate = updatedStudent.Birthdate;
			dbcontext.SaveChanges();
		}
		public void Delete(Student student)
		{
			dbcontext.Students.Remove(student);
			dbcontext.SaveChanges();
		}

		public Student TryGetById(int id)
		{
			return dbcontext.Students.FirstOrDefault(c => c.Id == id);
		}
	}
}
