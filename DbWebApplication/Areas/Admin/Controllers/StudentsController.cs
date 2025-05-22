using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class StudentsController : Controller
	{
		private readonly IStudentsRepository studentsRepository;
		private readonly UserManager<User> userManager;

		public StudentsController(IStudentsRepository studentsRepository, UserManager<User> userManager)
		{
			this.studentsRepository = studentsRepository;
			this.userManager = userManager;
		}

		public IActionResult Index()
		{
			var students = studentsRepository.GetAll();
			return View(students);
		}

		public IActionResult AddStudent()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ConfirmAddStudent(StudentViewModel studentViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(studentViewModel);
			}

			var student = new Student
			{
				UserName = studentViewModel.UserName,
				Email = studentViewModel.Email,
				Birthdate = studentViewModel.Birthdate,
			};

			studentsRepository.Add(student, studentViewModel.Password);

			return RedirectToAction("Index");
		}


		public IActionResult DeleteStudent(string studentId)
		{
			var student = studentsRepository.TryGetById(studentId);
			studentsRepository.Delete(student);
			return RedirectToAction("Index");
		}

		public IActionResult EditStudent(string studentId)
		{

			var student = studentsRepository.TryGetById(studentId);
			return View(student);
		}

		public IActionResult ConfirmEditStudent(string studentId, Student updatedStudent)
		{
			var student = studentsRepository.TryGetById(studentId);
			if (student.Email != updatedStudent.Email)
			{
				var existingUser = userManager.FindByEmailAsync(updatedStudent.Email).Result;
				if (existingUser != null && existingUser.Id != student.Id)
				{
					ModelState.AddModelError("Email", "Этот email уже используется.");
					return View(updatedStudent);
				}

				student.Email = updatedStudent.Email;
			}
			studentsRepository.Update(student, updatedStudent);
			return View();
		}
	}
}
