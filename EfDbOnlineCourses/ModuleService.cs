using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class ModuleService : IModuleService
	{
		private readonly DatabaseContext databaseContext;
		private readonly ICoursesRepository coursesRepository;
		public ModuleService(DatabaseContext databaseContext, ICoursesRepository coursesRepository)
		{
			this.databaseContext = databaseContext;
			this.coursesRepository = coursesRepository;
		}
		public List<Module> TryGetByCourseId(int courseId)
		{
			return databaseContext.Modules.Include(m => m.Lessons).Include(m => m.Course).Where(m => m.Course.Id == courseId).ToList();
		}

		public Lesson TryGetLessonById(int lessonId)
		{
			return databaseContext.Lessons.Include(l => l.Module).FirstOrDefault(l => l.Id == lessonId);
		}

		public Module TryGetById(int moduleId)
		{
			return databaseContext.Modules.Include(m => m.Lessons).Include(m => m.Course).FirstOrDefault(m => m.Id == moduleId);
		}

		public void AddLessonToModule(int moduleId, Lesson lesson)
		{
			var module = TryGetById(moduleId);
			module.Lessons.Add(lesson);
			databaseContext.SaveChanges();
		}
		public void RemoveLesson(int lessonId)
		{
			var lesson = databaseContext.Lessons.FirstOrDefault(l => l.Id == lessonId);
			databaseContext.Lessons.Remove(lesson);
			databaseContext.SaveChanges();
		}

		public void AddModule(Module module, int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			course.Modules.Add(module);
			databaseContext.SaveChanges();
		}

		public void RemoveModule(int moduleId)
		{
			var module = TryGetById(moduleId);
			databaseContext.Modules.Remove(module);
			databaseContext.SaveChanges();
		}

		public void EditModule(Module updatedModule, int moduleId)
		{
			var module = TryGetById(moduleId);

			module.Title = updatedModule.Title;
			databaseContext.SaveChanges();
		}


		public void EditLesson(Lesson updatedLesson, int lessonId)
		{
			var lesson = TryGetLessonById(lessonId);

			lesson.Title = updatedLesson.Title;
			lesson.Content = updatedLesson.Content;
			databaseContext.SaveChanges();
		}

	}
}
