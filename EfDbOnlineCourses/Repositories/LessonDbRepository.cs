using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Repositories
{
	public class LessonDbRepository : ILessonRepository
	{

		private readonly DatabaseContext databaseContext;
		private readonly IModuleRepository moduleService;
		public LessonDbRepository(DatabaseContext databaseContext, IModuleRepository moduleService)
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
			lesson.Module = module;
			module.Lessons.Add(lesson);
			databaseContext.SaveChanges();
		}

		public void RemoveLesson(int lessonId)
		{
			var lesson = TryGetLessonById(lessonId);
			databaseContext.Lessons.Remove(lesson);
			databaseContext.SaveChanges();
		}

		public Lesson TryGetLessonById(int lessonId)
		{
			return databaseContext.Lessons.Include(l => l.Module).ThenInclude(m => m.Course).FirstOrDefault(l => l.Id == lessonId);
		}

		public List<Lesson> TryGetLessonsByModuleId(int moduleId)
		{
			return databaseContext.Lessons.Include(l => l.Module).ThenInclude(m => m.Course).Where(l => l.Module.Id == moduleId).ToList();
		}


	}
}
