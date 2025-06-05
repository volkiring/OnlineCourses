using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class LessonService
	{

		private readonly DatabaseContext databaseContext;
		private readonly IModuleService moduleService;
		public LessonService(DatabaseContext databaseContext, IModuleService moduleService)
		{
			this.databaseContext = databaseContext;
			this.moduleService = moduleService;
		}

		public void EditLesson(Lesson updatedLesson, int lessonId)
		{
			var lesson = TryGetLessonById(lessonId);

			lesson.Title = updatedLesson.Title;
			lesson.Content = updatedLesson.Content;
			databaseContext.SaveChanges();
		}


		public void AddLessonToModule(int moduleId, Lesson lesson)
		{
			var module = moduleService.TryGetById(moduleId);
			module.Lessons.Add(lesson);
			databaseContext.SaveChanges();
		}

		public void RemoveLesson(int lessonId)
		{
			var lesson = databaseContext.Lessons.FirstOrDefault(l => l.Id == lessonId);
			databaseContext.Lessons.Remove(lesson);
			databaseContext.SaveChanges();
		}

		public Lesson TryGetLessonById(int lessonId)
		{
			return databaseContext.Lessons.Include(l => l.Module).FirstOrDefault(l => l.Id == lessonId);
		}

		public List<Lesson> TryGetLessonsByModuleId(int moduleId)
		{
			return databaseContext.Lessons.Include(l => l.Module).Where(l => l.Module.Id == moduleId).ToList();
		}


	}
}
