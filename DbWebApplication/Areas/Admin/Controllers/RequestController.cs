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
			if (request == null)
			{
				TempData["Error"] = "Заявка не найдена.";
				return RedirectToAction("Index");
			}

			if (request.Type.Name == "Заявка на становление преподавателем")
			{
				var user = usersService.TryGetUserByName(userName);
				if (user == null)
				{
					TempData["Error"] = "Пользователь не найден.";
					return RedirectToAction("Index");
				}

				try
				{
					teachersRepository.Add(user, request.Specialty);
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
