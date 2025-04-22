using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class CoursesController : Controller
	{
		private readonly ICoursesRepository coursesDbRepository;

		public CoursesController(ICoursesRepository coursesDbRepository)
		{
			this.coursesDbRepository = coursesDbRepository;
		}
		public IActionResult Index()
		{
			var courses = coursesDbRepository.GetAll();
			return View(courses);
		}

		public IActionResult AddCourse()
		{
			return View();
		}

		public IActionResult ConfirmAddCourse(Course course)
		{
			coursesDbRepository.Add(course);
			return View();
		}

		public IActionResult DeleteCourse(int courseId)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			coursesDbRepository.Delete(course);
			return RedirectToAction("Index");
		}

		public IActionResult EditCourse(int courseId)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			return View(course);
		}

		public IActionResult ConfirmEditCourse(int courseId, string title, string description)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			coursesDbRepository.Update(course, title, description);
			return View();
		}
	}
}
