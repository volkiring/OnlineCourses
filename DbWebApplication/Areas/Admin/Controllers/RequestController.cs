using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RequestController : Controller
	{
		private readonly IRequestsRepository requestsRepository;
		private readonly ITeachersRepository teachersRepository;
		private readonly ISpecialitiesRepository specialitiesRepository;
		private readonly IUsersService usersService;

		public RequestController(IRequestsRepository requestsRepository, ITeachersRepository teachersRepository, ISpecialitiesRepository specialitiesRepository, IUsersService usersService)
		{
			this.requestsRepository = requestsRepository;
			this.teachersRepository = teachersRepository;
			this.specialitiesRepository = specialitiesRepository;
			this.usersService = usersService;
		}
		public IActionResult Index()
		{
			var requests = requestsRepository.GetAll().OrderBy(x => x.Status).ToList();
			return View(requests);
		}

		public IActionResult Detail(int requestId)
		{
			var request = requestsRepository.TryGetById(requestId);
			return View(request);
		}

		public IActionResult Accept(int requestId, string userName)
		{
			var request = requestsRepository.TryGetById(requestId);

			if (request.Type.Name == "Заявка на становление преподавателем")
			{
				var student = usersService.TryGetUserByName(userName);

				var teacher = new Teacher()
				{
					Id = student.Id, 
					UserName = student.UserName,
					PasswordHash = student.PasswordHash,
					Email = student.Email,
					Courses = student.Courses,
					Specialty = request.Specialty
				};

				string password = null;
				teachersRepository.Add(teacher, password);
				requestsRepository.Accept(request);
			}


			return RedirectToAction("Index");
		}


		public ActionResult Deny(int requestId)
		{
			var request = requestsRepository.TryGetById(requestId);
			requestsRepository.Deny(request);
			return RedirectToAction("Index");
		}
	}
}
