using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class ModuleController : Controller
	{
		private readonly IModuleService moduleService;
		private readonly ICoursesRepository coursesRepository;

		public ModuleController(IModuleService moduleService, ICoursesRepository coursesRepository)
		{
			this.moduleService = moduleService;
			this.coursesRepository = coursesRepository;
		}

		public IActionResult Index(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var modules = course.Modules;	
			if (modules == null) return NotFound();
			return View(modules);
		}

		public IActionResult Create(int courseId)
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Module module, int courseId)
		{
			moduleService.AddModule(module, courseId);
			return RedirectToAction("Index", new { courseId });

		}

		[HttpGet]
		public IActionResult EditModule(int moduleId)
		{
			var module = moduleService.TryGetById(moduleId);
			return View(module);
		}

		[HttpPost]
		public IActionResult DeleteModule(int moduleId, int courseId)
		{
			var module = moduleService.TryGetById(moduleId);
			return RedirectToAction("Index", new { courseId });

		}


	}
}
