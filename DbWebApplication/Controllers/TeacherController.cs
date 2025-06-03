using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	[Authorize(Roles = "Teacher")]
	public class TeacherController : Controller
	{
		private readonly IUsersService usersService;
		public TeacherController(IUsersService usersService)
		{
			this.usersService = usersService;
		}
		public IActionResult Index(string userName)
		{
			var courses = usersService.GetTeacherCoursesByName(userName);
			return View(courses);
		}
	}
}
