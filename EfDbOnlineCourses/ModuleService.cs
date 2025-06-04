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

		public Module TryGetById(int courseId)
		{
			return databaseContext.Modules.Include(m => m.Lessons).Include(m => m.Course).FirstOrDefault(m => m.Id == courseId);
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

	}
}
