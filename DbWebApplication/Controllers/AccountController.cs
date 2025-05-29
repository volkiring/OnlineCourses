using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private readonly DatabaseContext databaseContext;
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext databaseContext)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.databaseContext = databaseContext;

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
			var student = new Student()
			{
				UserName = register.UserName,
				Birthdate = register.Birthdate,
				PasswordHash = register.Password,
				Email = register.Email
			};

			var result = userManager.CreateAsync(student, register.Password).Result;

			if (result.Succeeded)
			{
				signInManager.SignInAsync(student, false).Wait();
				return RedirectToAction("Index", "Home");
			}

			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
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
