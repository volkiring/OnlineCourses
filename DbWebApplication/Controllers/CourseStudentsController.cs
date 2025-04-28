using EfDbOnlineCourses;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
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
			var students = studentsRepository.GetAll();
			var course = coursesRepository.TryGetById(courseId);
			return View((course, students));
		}

		public IActionResult ConfirmAddStudentToCourse(int courseId,  int studentId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var student = studentsRepository.TryGetById(studentId);
			coursesRepository.AddStudentToCourse(course, student);
			return RedirectToAction("Index", new { courseId });
		}

	}
}
