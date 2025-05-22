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

		public IActionResult AddTeacher()
		{
			var specialities = specialitiesRepository.GetAll();
			return View(specialities);
		}

		public IActionResult ConfirmAddTeacher(TeacherViewModel teacherViewModel)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Ошибка во входных данных");
				return View(teacherViewModel);
			}

			var specialty = specialitiesRepository.TryGetById(teacherViewModel.specialtyId);

			var teacher = new Teacher()
			{
				UserName = teacherViewModel.UserName,
				Email = teacherViewModel.Email,
				Birthdate = teacherViewModel.Birthdate,
				Specialty = specialty
			};

			teachersRepository.Add(teacher, teacherViewModel.Password);

			return RedirectToAction("Index");
		}

		public IActionResult DeleteTeacher(string teacherId)
		{
			var teacher = teachersRepository.TryGetById(teacherId);
			teachersRepository.Delete(teacher);
			return RedirectToAction("Index");
		}

		public IActionResult EditTeacher(string teacherId)
		{
			var teacher = teachersRepository.TryGetById(teacherId);
			var specialities = specialitiesRepository.GetAll();

			ViewBag.SelectedSpecialtyId = teacher.Specialty?.Id;
			return View((teacher,specialities));
		}

		public IActionResult ConfirmEditTeacher(string teacherId, Teacher updatedTeacher, int specialtyId)
		{
			var teacher = teachersRepository.TryGetById(teacherId);
			var specialty = specialitiesRepository.TryGetById(specialtyId);
			updatedTeacher.Specialty = specialty;

			if (teacher.Email != updatedTeacher.Email)
			{
				var existingUser = userManager.FindByEmailAsync(updatedTeacher.Email).Result;
				if (existingUser != null && existingUser.Id != teacher.Id)
				{
					ModelState.AddModelError("Email", "Этот email уже используется.");
					return View(updatedTeacher);
				}

				teacher.Email = updatedTeacher.Email;
			}
			teachersRepository.Update(teacher, updatedTeacher);

			return View();
		}
	}
}
