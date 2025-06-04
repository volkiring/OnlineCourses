using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	[Authorize(Roles = "Teacher")]
	public class TeacherController : Controller
	{
		private readonly IUsersService usersService;
		private readonly ICoursesRepository coursesRepository;
		private readonly IWebHostEnvironment webHostEnvironment;
		public TeacherController(IUsersService usersService, ICoursesRepository coursesRepository, IWebHostEnvironment webHostEnvironment)
		{
			this.usersService = usersService;
			this.webHostEnvironment = webHostEnvironment;
			this.coursesRepository = coursesRepository;
		}
		public IActionResult Index(string userName)
		{
			var courses = usersService.GetTeacherCoursesByName(userName);
			return View(courses);
		}

		public IActionResult DeleteCourse(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			coursesRepository.Delete(course);
			return RedirectToAction("Index", new {userName = User.Identity.Name});
		}

		public IActionResult EditCourse(int courseId)
		{
			var course = coursesRepository.TryGetById(courseId);
			return View(course);
		}

		public IActionResult ConfirmEditCourse(int courseId, Course updatedCourse, IFormFile? ImageFile)
		{

			if (updatedCourse.EndDate < updatedCourse.StartDate)
			{
				ModelState.AddModelError("", "Дата окончания не может быть раньше даты начала.");
				var originalCourse = coursesRepository.TryGetById(courseId);
				return View("EditCourse", originalCourse);
			}

			var course = coursesRepository.TryGetById(courseId);

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

			coursesRepository.Update(course, updatedCourse, imagePath);

			return View();
		}

		[HttpGet]
		public IActionResult AddCourse()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddCourse(Course course, IFormFile ImageFile, string userName)
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

			var user = usersService.TryGetUserByName(User.Identity.Name);
			var teacher = user.Teacher;

			coursesRepository.Add(course);
			coursesRepository.AddTeacherToCourse(course, teacher);
			TempData["Success"] = $"Курс {course.Title} успешно создан!";
			return RedirectToAction("Index", new { userName = User.Identity.Name});	
		}


	}
}
