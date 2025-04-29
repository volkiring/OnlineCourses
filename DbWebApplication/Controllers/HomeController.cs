using DbWebApplication.Models;
using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DbWebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICoursesRepository coursesRepository;

		public HomeController(ICoursesRepository coursesRepository)
		{
			this.coursesRepository = coursesRepository;
		}
		public IActionResult Index()
		{
			var courses = coursesRepository.GetAll();	
			return View(courses);
		}

		public IActionResult Details(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			return View(course);
		}
	}
}
