using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class RequestController : Controller
    {
        private readonly IRequestsRepository requestsRepository;
        private readonly ITeachersRepository teachersRepository;
        private readonly IUsersService usersService;
		private readonly UserManager<User> userManager;

        public RequestController(IRequestsRepository requestsRepository, ITeachersRepository teachersRepository, IUsersService usersService, UserManager<User> userManager)
        {
            this.requestsRepository = requestsRepository;
            this.teachersRepository = teachersRepository;
            this.usersService = usersService;
			this.userManager = userManager;
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
			var user = usersService.TryGetUserByName(userName);
			if (request == null)
			{
				TempData["Error"] = "Заявка не найдена.";
				return RedirectToAction("Index");
			}

			if (request.Type.Name == Constants.TeacherRequest)
			{
				if (user == null)
				{
					TempData["Error"] = "Пользователь не найден.";
					return RedirectToAction("Index");
				}

				try
				{
					teachersRepository.Add(user, request.Specialty);
					userManager.AddToRoleAsync(user, Constants.TeacherRoleName).Wait();
					requestsRepository.Accept(request);
				}
				catch (InvalidOperationException ex)
				{
					TempData["Error"] = "Ошибка: " + ex.Message;
					return RedirectToAction("Index");
				}
			}

			if (request.Type.Name == Constants.AdminRequest)
			{
				if (user == null)
				{
					TempData["Error"] = "Пользователь не найден.";
					return RedirectToAction("Index");
				}

				try
				{
					userManager.AddToRoleAsync(user, Constants.AdminRoleName).Wait();
					requestsRepository.Accept(request);
				}

				catch (InvalidOperationException ex)
				{
					TempData["Error"] = "Ошибка: " + ex.Message;
					return RedirectToAction("Index");
				}

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
