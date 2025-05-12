using EfDbOnlineCourses.Models;
using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace DbWebApplication.Controllers
{
	public class TeachersController : Controller
	{
		private readonly ITeachersRepository teachersRepository;
		private readonly UserManager<Teacher> userManager;

		public TeachersController(ITeachersRepository teachersRepository)
		{
			this.teachersRepository = teachersRepository;
		}

		public IActionResult Index()
		{
			var teachers = teachersRepository.GetAll();
			return View(teachers);
		}

		public IActionResult AddTeacher()
		{
			return View();
		}

		public IActionResult ConfirmAddTeacher(Teacher teacher)
		{
			if (!ModelState.IsValid)
				return View();

			var result = userManager.CreateAsync(teacher, teacher.Password).Result;

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
			return View(teacher);
		}

		public IActionResult DeleteTeacher(int teacherId)
		{
			var teacher = teachersRepository.TryGetById(teacherId);
			teachersRepository.Delete(teacher);
			return RedirectToAction("Index");
		}

		public IActionResult EditTeacher(int teacherId)
		{
			var course = teachersRepository.TryGetById(teacherId);
			return View(course);
		}

		public IActionResult ConfirmEditTeacher(int teacherId, Teacher updatedTeacher)
		{
			var teacher = teachersRepository.TryGetById(teacherId);
			teachersRepository.Update(teacher, updatedTeacher);
			return View();
		}

	}
}
