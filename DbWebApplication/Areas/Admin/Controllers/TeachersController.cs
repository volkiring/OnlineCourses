using EfDbOnlineCourses.Models;
using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DbWebApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TeachersController : Controller
	{
		private readonly ITeachersRepository teachersRepository;

		public TeachersController(ITeachersRepository teachersRepository, UserManager<User> userManager, ISpecialitiesRepository specialitiesRepository	)
		{
			this.teachersRepository = teachersRepository;
		}

		public IActionResult Index()
		{
			var teachers = teachersRepository.GetAll();
			return View(teachers);
		}

		public IActionResult DeleteTeacher(string teacherId)
		{
			var teacher = teachersRepository.TryGetById(teacherId);
			teachersRepository.Delete(teacher);
			return RedirectToAction("Index");
		}


	}
}
