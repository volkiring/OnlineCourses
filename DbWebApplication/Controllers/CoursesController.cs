using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace DbWebApplication.Controllers
{
	public class CoursesController : Controller
	{
		private readonly IUsersService usersService;
		private readonly ICoursesRepository coursesRepository;

		public CoursesController(IUsersService usersService, ICoursesRepository coursesRepository)
		{
			this.usersService = usersService;
			this.coursesRepository = coursesRepository;
		}

		public IActionResult Index()
		{
			string userId = User.Identity.IsAuthenticated
	? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	: null;

			if (userId == null)
			{
				return Unauthorized();
			}
			var userCourses = usersService.GetUserCoursesById(userId);

			return View(userCourses);
		}

		public IActionResult AddStudentToCourse(int courseId)
		{
			string userId = User.Identity.IsAuthenticated
? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
: null;

			if (userId == null)
			{
				return Unauthorized();
			}

			var student = usersService.GetUserById(userId);
			var course = coursesRepository.TryGetById(courseId);

			if (student.Courses.Any(c => c.Id == course.Id))
			{
				return RedirectToAction("Error");
			}

			coursesRepository.AddStudentToCourse(course, student);

			string courseTitle = course.Title;
			return View(model : courseTitle);
		}

		public IActionResult Error()
		{
			{
				return View();
			}
		}

		public IActionResult RemoveStudentFromCourse(int courseId)
		{
			string userId = User.Identity.IsAuthenticated
? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
: null;

			if (userId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var student = usersService.GetUserById(userId);
			var course = coursesRepository.TryGetById(courseId);

			coursesRepository.DeleteStudentToCourse(course, student);

			return RedirectToAction("Index");
		}
	}
}
