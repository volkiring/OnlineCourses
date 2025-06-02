using DbWebApplication.Models;
using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace DbWebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICoursesRepository coursesRepository;
		private readonly IUsersService usersService;

		public HomeController(ICoursesRepository coursesRepository, IUsersService usersService)
		{
			this.coursesRepository = coursesRepository;
			this.usersService = usersService;
		}
		public IActionResult Index()
		{
			var courses = coursesRepository.GetAll();	
			return View(courses);
		}

		public IActionResult Details(int courseId, string userName)
		{
			var course = coursesRepository.TryGetById(courseId);

			var isCourseExist = false;
			if (userName != null)
			{
				var userCourses = usersService.GetUserCoursesByName(userName);
				if (userCourses.Contains(course))
				{
					isCourseExist = true;
				}
			}


			return View((course, isCourseExist));
		}
	}
}
