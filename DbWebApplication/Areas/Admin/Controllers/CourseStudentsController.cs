using EfDbOnlineCourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CourseStudentsController : Controller
	{
		private readonly ICoursesRepository coursesRepository;
		private readonly IStudentsRepository studentsRepository;

		public CourseStudentsController(ICoursesRepository coursesRepository,IStudentsRepository studentsRepository)
		{
			this.coursesRepository = coursesRepository;
			this.studentsRepository = studentsRepository;

		}
		public IActionResult Index(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			return View(course);
		}

		public IActionResult AddStudentToCourse(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var students = studentsRepository.GetAll().Where(s => !(s.User.Courses.Contains(course))).ToList();
			return View((course, students));
		}

		public IActionResult ConfirmAddStudentToCourse(int courseId,  string studentId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var student = studentsRepository.TryGetById(studentId);
			coursesRepository.AddStudentToCourse(course, student);
			return RedirectToAction("Index", new { courseId });
		}

		public IActionResult DeleteStudentToCourse(int courseId, string studentId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var student = studentsRepository.TryGetById(studentId);
			coursesRepository.DeleteStudentToCourse(course, student);
			return RedirectToAction("Index", new { courseId });
		
		}
	}
}
