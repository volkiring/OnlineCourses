using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class SpecialitiesController : Controller
	{
		private readonly ISpecialitiesRepository specialitiesRepository;

		public SpecialitiesController(ISpecialitiesRepository specialtyRepository)
		{
			this.specialitiesRepository = specialtyRepository;
		}
		public IActionResult Index()
		{
			var specialities = specialitiesRepository.GetAll();
			return View(specialities);
		}

		public IActionResult AddSpecialty()
		{
			return View();
		}

		public IActionResult ConfirmAddSpecialty(Specialty specialty)
		{
			specialitiesRepository.Add(specialty);
			return RedirectToAction("Index");
		}

		public IActionResult UpdateSpecialty(int specialtyId)
		{
			var specialty = specialitiesRepository.TryGetById(specialtyId);
			return View(specialty);
		}

		public IActionResult ConfirmUpdateSpecialty(int specialtyId, Specialty updatedSpecialty)
		{
			var specialty = specialitiesRepository.TryGetById(specialtyId);
			specialitiesRepository.Update(specialty, updatedSpecialty);
			return RedirectToAction("Index");
		}

	}
}
