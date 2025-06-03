using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class RequestController : Controller
	{
		private readonly IUsersService usersService;
		private readonly IRequestsRepository requestsRepository;
		private readonly ISpecialitiesRepository specialitiesRepository;
		private readonly IRequestTypeRepository requestTypeRepository;
		private readonly UserManager<User> userManager;	
		public RequestController(IUsersService usersService, IRequestsRepository requestsRepository, ISpecialitiesRepository specialitiesRepository, IRequestTypeRepository requestTypeRepository, UserManager<User> userManager)
		{
			this.usersService = usersService;
			this.requestsRepository = requestsRepository;
			this.specialitiesRepository = specialitiesRepository;
			this.requestTypeRepository = requestTypeRepository;
			this.userManager = userManager;
		}

		public IActionResult Index(string userName)
		{
			var user = usersService.TryGetUserByName(userName);
			if (user == null)
			{
				return RedirectToAction("Login","Account");
			}

			return View(user.Requests);
		}

		public IActionResult Add()
		{
			ViewBag.Specialties = specialitiesRepository.GetAll();
			ViewBag.RequestTypes = requestTypeRepository.GetAll();
			return View();
		}

		public IActionResult ConfirmAdd(Request requestViewModel, string userName)
		{
			var user = usersService.TryGetUserByName(userName);
			var type = requestTypeRepository.TryGetById(requestViewModel.Type.Id);
			if (type.Name == Constants.TeacherRequest)
			{
				if (user.Teacher is not null)
				{
					return RedirectToAction("ErrorTeacher");
				}
			}

			if (type.Name == Constants.AdminRequest)
			{
				if (userManager.IsInRoleAsync(user, Constants.AdminRoleName).Result)
				{
					return RedirectToAction("ErrorAdmin");
				}
			}


			var request = new Request()
			{
				Message = requestViewModel.Message,
				Specialty = specialitiesRepository.TryGetById(requestViewModel.Specialty.Id),
				SubmittedAt = DateTime.Now,
				Status = 0,
				Type = type,
				User = user,

			};
			requestsRepository.Add(request);
			return RedirectToAction("Index", new {user.UserName});	
		}

		public IActionResult ErrorTeacher()
		{
			return View();
		}

		public IActionResult ErrorAdmin()
		{
			return View();
		}


	}
}
