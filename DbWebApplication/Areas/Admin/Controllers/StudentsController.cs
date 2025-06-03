using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class StudentsController : Controller
	{
		private readonly IStudentsRepository studentsRepository;

		public StudentsController(IStudentsRepository studentsRepository, UserManager<User> userManager)
		{
			this.studentsRepository = studentsRepository;
		}

		public IActionResult Index()
		{
			var students = studentsRepository.GetAll();
			return View(students);
		}

		public IActionResult DeleteStudent(string studentId)
		{
			var student = studentsRepository.TryGetById(studentId);
			studentsRepository.Delete(student);
			return RedirectToAction("Index");
		}

	}
}
