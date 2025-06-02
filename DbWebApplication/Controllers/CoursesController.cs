using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DbWebApplication.Controllers
{
	public class CoursesController : Controller
	{
		private readonly IUsersService usersService;
		private readonly ICoursesRepository coursesRepository;
		private readonly IStudentsRepository studentsRepository;

		public CoursesController(IUsersService usersService, ICoursesRepository coursesRepository, IStudentsRepository studentsRepository)
		{
			this.usersService = usersService;
			this.coursesRepository = coursesRepository;
			this.studentsRepository = studentsRepository;
		}

		public IActionResult Index(string userName)
		{ 

			if (userName == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var userCourses = usersService.GetUserCoursesByName(userName);

			return View(userCourses);
		}

		public IActionResult AddStudentToCourse(int courseId, string userName)
		{
			if (userName == null)
			{
				return RedirectToAction("Login","Account");
			}

			var student = studentsRepository.TryGetById(userName);
			var course = coursesRepository.TryGetById(courseId);

			if (student.User.Courses.Any(c => c.Id == course.Id))
			{
				return RedirectToAction("Error");
			}

			coursesRepository.AddStudentToCourse(course, student);

			string courseTitle = course.Title;
			return View(model: courseTitle);
		}

		public IActionResult Error()
		{
			return View();
		}

		public IActionResult RemoveStudentFromCourse(int courseId, string userName)
		{

			if (userName == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var student = studentsRepository.TryGetById(userName);
			var course = coursesRepository.TryGetById(courseId);

			coursesRepository.DeleteStudentToCourse(course, student);

			return RedirectToAction("Index");
		}
	}
}
