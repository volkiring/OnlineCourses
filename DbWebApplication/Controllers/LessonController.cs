using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class LessonController : Controller
	{
		private readonly IModuleRepository moduleService;
		private readonly ICoursesRepository coursesRepository;
		private readonly ILessonRepository lessonRepository;

		public LessonController(IModuleRepository moduleService, ICoursesRepository coursesRepository, ILessonRepository lessonRepository)
		{
			this.moduleService = moduleService;
			this.coursesRepository = coursesRepository;
			this.lessonRepository = lessonRepository;
		}

		public IActionResult Index(int moduleId)
		{
			var lessons = lessonRepository.TryGetLessonsByModuleId(moduleId);
			return View((lessons,moduleId));
		}

		[HttpGet]
		public IActionResult Create(int moduleId)
		{
			ViewBag.ModuleId = moduleId;
			return View();
		}

		[HttpPost]
		public IActionResult Create(int moduleId, Lesson lesson)
		{
			ViewBag.ModuleId = moduleId;
			moduleService.AddLessonToModule(moduleId, lesson);
			return RedirectToAction("Index", new { moduleId });
		}

		public IActionResult Edit(int lessonId, int moduleId)
		{
			ViewBag.ModuleId = moduleId;
			var lesson = lessonRepository.TryGetLessonById(lessonId);
			return View(lesson);
		}

		[HttpPost]
		public IActionResult Edit(int lessonId, Lesson updatedLesson, int moduleId)
		{
			ViewBag.ModuleId = moduleId;
			lessonRepository.EditLesson(updatedLesson, lessonId);
			return RedirectToAction("Index", new { moduleId});
		}

		[HttpPost]
		public IActionResult Delete(int lessonId)
		{
			var moduleId = lessonRepository.TryGetLessonById(lessonId).Module.Id;
			moduleService.RemoveLesson(lessonId);
			return RedirectToAction("Index" , new {moduleId});
		}

		public IActionResult Content(int lessonId)
		{
			var lesson = lessonRepository.TryGetLessonById(lessonId);
			return View(lesson);
			
		}
	}
}
