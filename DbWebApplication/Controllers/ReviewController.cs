using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IReviewRepository reviewRepository;
		private readonly IStudentsRepository studentsRepository;
		private readonly ICoursesRepository coursesRepository;
		public ReviewController(IReviewRepository reviewRepository, IStudentsRepository studentsRepository, ICoursesRepository coursesRepository)
		{
			this.reviewRepository = reviewRepository;
			this.studentsRepository = studentsRepository;
			this.coursesRepository = coursesRepository;
		}
		public IActionResult Index(int courseId)
		{
			var reviews = reviewRepository.TryGetByCourseId(courseId);
			ViewBag.CourseId = courseId;
			return View(reviews);
		}
		
		public IActionResult Create(int courseId, string userName, Review review)
		{
			var student = studentsRepository.TryGetByUserName(userName);
			var course = coursesRepository.TryGetById(courseId);
			reviewRepository.Add(course, student, review);
			return RedirectToAction("Index", new {courseId});
		}
	}
}
