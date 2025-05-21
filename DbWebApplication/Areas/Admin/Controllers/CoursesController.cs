using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CoursesController : Controller
	{
		private readonly ICoursesRepository coursesDbRepository;
		private readonly IWebHostEnvironment webHostEnvironment;

		public CoursesController(ICoursesRepository coursesDbRepository, IWebHostEnvironment webHostEnvironment)
		{
			this.coursesDbRepository = coursesDbRepository;
			this.webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			var courses = coursesDbRepository.GetAll();
			return View(courses);
		}

		public IActionResult AddCourse()
		{
			return View();
		}

		public IActionResult ConfirmAddCourse(Course course, IFormFile ImageFile)
		{

			if (course.EndDate < course.StartDate)
			{
				ModelState.AddModelError("", "Дата окончания не может быть раньше даты начала.");
				return View("AddCourse");
			}

			var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/courses");
			Directory.CreateDirectory(uploadsFolder);

			var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
			var filePath = Path.Combine(uploadsFolder, uniqueFileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				ImageFile.CopyTo(stream);
			}

			course.ImagePath = "/images/courses/" + uniqueFileName;

			coursesDbRepository.Add(course);
			return View();
		}

		public IActionResult DeleteCourse(int courseId)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			coursesDbRepository.Delete(course);
			return RedirectToAction("Index");
		}

		public IActionResult EditCourse(int courseId, IFormFile ImageFile)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			return View(course);
		}

		public IActionResult ConfirmEditCourse(int courseId, Course updatedCourse, IFormFile? ImageFile)
		{

			if (updatedCourse.EndDate < updatedCourse.StartDate)
			{
				ModelState.AddModelError("", "Дата окончания не может быть раньше даты начала.");
				var originalCourse = coursesDbRepository.TryGetById(courseId);
				return View("EditCourse", originalCourse); 
			}

			var course = coursesDbRepository.TryGetById(courseId);

			string? imagePath = null;
			if (ImageFile is not null)
			{
				var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/courses");
				Directory.CreateDirectory(uploadsFolder);
				var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
				var filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					ImageFile.CopyTo(stream);
				}

				imagePath = "/images/courses/" + uniqueFileName;
			}

			coursesDbRepository.Update(course, updatedCourse, imagePath);

			return View();
		}

		public IActionResult StudentsByCourse(int courseId)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			var students = course.Students;
			return View(students);
		}

		public IActionResult TeachersByCourse(int courseId)
		{
			var course = coursesDbRepository.TryGetById(courseId);
			var teachers = course.Teachers;
			return View(teachers);
		}
	}
}
