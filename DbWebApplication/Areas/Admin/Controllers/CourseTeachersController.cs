using EfDbOnlineCourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CourseTeachersController : Controller
	{
		private readonly ICoursesRepository coursesRepository;
		private readonly ITeachersRepository teachersRepository;

		public CourseTeachersController(ICoursesRepository coursesRepository, ITeachersRepository teachersRepository)
		{
			this.coursesRepository = coursesRepository;
			this.teachersRepository = teachersRepository;
		}

		public IActionResult Index(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			return View(course);
		}
		public IActionResult AddTeacherToCourse(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var teachers = teachersRepository.GetAll().Where(t => !t.User.Teacher.CoursesTaught.Contains(course)).ToList();
			return View((course, teachers));
		}

		public IActionResult ConfirmAddTeacherToCourse(int courseId, string teacherId)
		{
			var teacher = coursesRepository.TryGetById(courseId);
			var student = teachersRepository.TryGetById(teacherId);
			coursesRepository.AddTeacherToCourse(teacher, student);
			return RedirectToAction("Index", new { courseId });
		}

		public IActionResult DeleteTeacherToCourse(int courseId, string teacherId)
		{
			var course = coursesRepository.TryGetById(courseId);
			var teacher = teachersRepository.TryGetById(teacherId);
			coursesRepository.DeleteTeacherToCourse(course, teacher);
			return RedirectToAction("Index", new { courseId });
		}
	}
}
