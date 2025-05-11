using DbWebApplication.Models;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DbWebApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
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
			var user = new User { UserName = register.UserName, Email = register.Email };

			var result = userManager.CreateAsync(user, register.Password).Result;

			if (result.Succeeded)
			{
				signInManager.SignInAsync(user, false).Wait();
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
