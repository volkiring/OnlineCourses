using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	public class RequestController : Controller
	{
		private readonly IRequestsRepository requestsRepository;
		private readonly ITeachersRepository teachersRepository;
		private readonly ISpecialitiesRepository specialitiesRepository;
		private readonly UserManager<User> userManager;
		public RequestController(IRequestsRepository requestsRepository, ITeachersRepository teachersRepository, ISpecialitiesRepository specialitiesRepository, UserManager<User> userManager)
		{
			this.requestsRepository = requestsRepository;
			this.teachersRepository = teachersRepository;
			this.specialitiesRepository = specialitiesRepository;
			this.userManager = userManager;
		}
		public IActionResult Index()
		{
			var requests = requestsRepository.GetAll();
			return View(requests);
		}

		public IActionResult Detail(string id)
		{
			var request = requestsRepository.TryGetById(id);
			return View(request);
		}

		public IActionResult Accept(string requestId, User user, TeacherViewModel teacherViewModel)
		{
			var request = requestsRepository.TryGetById(requestId);

			if (request.Type.Name == "Заявка на становление преподавателем")
			{
				var teacher = new Teacher()
				{
					UserName = user.UserName,
					Id = requestId,
					PasswordHash = user.PasswordHash, 
					Email = user.Email,
					Courses = user.Courses,
					Specialty = specialitiesRepository.TryGetById(teacherViewModel.specialtyId)
				};

				var result = userManager.DeleteAsync(user).Result;
				if (result.Succeeded)
				{
					string password = null;
					teachersRepository.Add(teacher, password);
				}
			}
			request.Status = true;
			return RedirectToAction("Index");
		}

		public ActionResult Deny(string requestId)
		{
			var request = requestsRepository.TryGetById(requestId);
			request.Status = false;
			return RedirectToAction("Index");
		}
	}
}
