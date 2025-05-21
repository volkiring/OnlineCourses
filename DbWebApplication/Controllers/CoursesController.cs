using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class CoursesController : Controller
	{
		public IActionResult Index()
		{

			return View();
		}
	}
}
