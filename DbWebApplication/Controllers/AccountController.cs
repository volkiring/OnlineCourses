using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<User> signInManager;
		private readonly IStudentsRepository studentsRepository;
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext databaseContext, IStudentsRepository studentsRepository)
		{
			this.signInManager = signInManager;
			this.studentsRepository = studentsRepository;
		}

		[HttpGet]
		public IActionResult Login(string? returnUrl)
		{
			return View();
		}

		public IActionResult Login(Login login)
		{
			var result = signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false).Result;

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}

			else
			{
				ModelState.AddModelError("", "Неправильный логин или пароль");
			}

			return View(login);
		}

		[HttpGet]
		public IActionResult Register(string? returnUrl)
		{
			return View();
		}

		public IActionResult Register(Register register)
		{
			if (!ModelState.IsValid)
				return View(register);

			var user = new User
			{
				UserName = register.UserName,
				Email = register.Email,
				Birthdate = register.Birthdate
			};

			var result = studentsRepository.Add(user, register.Password, out var createdUser);

			if (result.Succeeded)
			{
				signInManager.SignInAsync(createdUser, isPersistent: true).Wait();
				return RedirectToAction("Index", "Home");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}

			return View(register);
		}

		public IActionResult Logout()
		{
			signInManager.SignOutAsync().Wait();
			return RedirectToAction("Index", "Home");
		}

	}
}
