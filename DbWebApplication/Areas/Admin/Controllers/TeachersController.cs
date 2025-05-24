using EfDbOnlineCourses.Models;
using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DbWebApplication.Models;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class TeachersController : Controller
	{
		private readonly ITeachersRepository teachersRepository;
		private readonly ISpecialitiesRepository specialitiesRepository;
		private readonly UserManager<User> userManager;

		public TeachersController(ITeachersRepository teachersRepository, UserManager<User> userManager, ISpecialitiesRepository specialitiesRepository	)
		{
			this.teachersRepository = teachersRepository;
			this.userManager = userManager;
			this.specialitiesRepository = specialitiesRepository;
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
