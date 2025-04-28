using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class CourseTeachersController : Controller
	{
		private readonly ICoursesRepository coursesRepository;

		public CourseTeachersController(ICoursesRepository coursesRepository)
		{
			this.coursesRepository = coursesRepository;
		}
		public IActionResult Index(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var teachers = course?.Teachers;
			return View(teachers);
		}
	}
}
