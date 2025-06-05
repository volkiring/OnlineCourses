using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class LessonController : Controller
	{
		private readonly IModuleService moduleService;
		private readonly ICoursesRepository coursesRepository;

		public LessonController(IModuleService moduleService, ICoursesRepository coursesRepository)
		{
			this.moduleService = moduleService;
			this.coursesRepository = coursesRepository;
		}

		public IActionResult CreateLesson(int moduleId)
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateLesson(int moduleId, Lesson lesson)
		{
			if (!ModelState.IsValid)
			{
				return View(lesson);
			}

			moduleService.AddLessonToModule(moduleId, lesson);
			return RedirectToAction("Index", new { courseId = moduleService.TryGetById(moduleId).Course.Id });
		}

		public IActionResult EditLesson(int id)
		{
			var module = moduleService.TryGetById(id);
			var lesson = module?.Lessons.FirstOrDefault(l => l.Id == id);
			if (lesson == null) return NotFound();
			return View(lesson);
		}

		[HttpPost]
		public IActionResult EditLesson(int lessonId, Lesson updatedLesson)
		{
			var lesson = moduleService.TryGetById(lessonId);
			if (lesson == null) return NotFound();

			return RedirectToAction("Index", new { courseId = lesson.Course.Id });
		}

		[HttpPost]
		public IActionResult DeleteLesson(int id)
		{
			moduleService.RemoveLesson(id);
			return RedirectToAction("Index");
		}
	}
}
