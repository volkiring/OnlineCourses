using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class StudentsController : Controller
	{
		private readonly IStudentsRepository studentsRepository;

		public StudentsController(IStudentsRepository studentsRepository)
		{
			this.studentsRepository = studentsRepository;
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

		public IActionResult ConfirmAddStudent(Student student)
		{
			studentsRepository.Add(student);
			return View();
		}

		public IActionResult DeleteStudent(int studentId)
		{
			var student = studentsRepository.TryGetById(studentId);
			studentsRepository.Delete(student);
			return RedirectToAction("Index");
		}

		public IActionResult EditStudent(int studentId)
		{
			var student = studentsRepository.TryGetById(studentId);
			return View(student);
		}

		public IActionResult ConfirmEditStudent(int studentId, Student updatedStudent)
		{
			var student = studentsRepository.TryGetById(studentId);
			studentsRepository.Update(student, updatedStudent);
			return View();
		}
	}
}
