﻿using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IStudentsRepository
	{
		void Add(Student student, string password);
		void Delete(Student student);
		List<User> GetAll();
		Student TryGetById(string id);
		void Update(Student student, Student updatedStudent);
	}
}