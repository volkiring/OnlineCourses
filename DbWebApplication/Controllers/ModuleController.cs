using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class ModuleController : Controller
	{
		private readonly IModuleRepository moduleService;
		private readonly ICoursesRepository coursesRepository;

		public ModuleController(IModuleRepository moduleService, ICoursesRepository coursesRepository)
		{
			this.moduleService = moduleService;
			this.coursesRepository = coursesRepository;
		}

		public IActionResult Index(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var modules = course.Modules;	
			if (modules == null) return NotFound();
			return View((modules, courseId));
		}

		public IActionResult Create(int courseId)
		{
			ViewBag.CourseId = courseId;
			return View();
		}

		[HttpPost]
		public IActionResult Create(Module module, int courseId)
		{
			ViewBag.CourseId = courseId;
			moduleService.AddModule(module, courseId);
			return RedirectToAction("Index", new { courseId });
		}


		public IActionResult Edit(int courseId, int moduleId)
		{
			var module = moduleService.TryGetById(moduleId);
			return View(module);
		}

		[HttpPost]
		public IActionResult Edit(int moduleId, Module updatedModule, int courseId)
		{
			var module = moduleService.TryGetById(moduleId);
			moduleService.EditModule(updatedModule, module);
			return RedirectToAction("Index", new { courseId });
		}

		[HttpPost]
		public IActionResult Delete(int moduleId, int courseId)
		{
			moduleService.RemoveModule(moduleId);
			return RedirectToAction("Index", new { courseId });
		}


	}
}
