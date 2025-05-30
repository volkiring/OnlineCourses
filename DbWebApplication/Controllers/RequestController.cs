﻿using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class RequestController : Controller
	{
		private readonly IUsersService usersService;
		private readonly IRequestsRepository requestsRepository;
		private readonly ISpecialitiesRepository specialitiesRepository;
		private readonly IRequestTypeRepository requestTypeRepository;
		public RequestController(IUsersService usersService, IRequestsRepository requestsRepository, ISpecialitiesRepository specialitiesRepository, IRequestTypeRepository requestTypeRepository)
		{
			this.usersService = usersService;
			this.requestsRepository = requestsRepository;
			this.specialitiesRepository = specialitiesRepository;
			this.requestTypeRepository = requestTypeRepository;
		}

		public IActionResult Index(string userName)
		{
			var user = usersService.TryGetUserByName(userName);
			if (user == null)
			{
				return Unauthorized();
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
			var request = new Request()
			{
				Message = requestViewModel.Message,
				Specialty = specialitiesRepository.TryGetById(requestViewModel.Specialty.Id),
				SubmittedAt = DateTime.Now,
				Status = 0,
				Type = requestTypeRepository.TryGetById(requestViewModel.Type.Id),
				User = user,

			};
			requestsRepository.Add(request);
			return RedirectToAction("Index", new {user.UserName});	
		}


	}
}
