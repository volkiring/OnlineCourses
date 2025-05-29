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

		public RequestController(IRequestsRepository requestsRepository, ITeachersRepository teachersRepository, ISpecialitiesRepository specialitiesRepository)
		{
			this.requestsRepository = requestsRepository;
			this.teachersRepository = teachersRepository;
			this.specialitiesRepository = specialitiesRepository;
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

		public IActionResult Accept(int requestId, Student student, int specialtyId)
		{
			var request = requestsRepository.TryGetById(requestId);

			if (request.Type.Name == "Заявка на становление преподавателем")
			{
				var teacher = new Teacher()
				{
					UserName = student.UserName,
					PasswordHash = student.PasswordHash,
					Email = student.Email,
					Courses = student.Courses,
					Specialty = specialitiesRepository.TryGetById(specialtyId)
				};

				string password = null;
				teachersRepository.Add(teacher, password);

			}
			request.Status = RequestStatus.Accepted;
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
