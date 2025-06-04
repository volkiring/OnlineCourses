using DbWebApplication.Models;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<User> signInManager;
		private readonly IStudentsRepository studentsRepository;
		private readonly IUsersService usersService;
		private readonly UserManager<User> userManager;
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IStudentsRepository studentsRepository, IUsersService usersService)
		{
			this.signInManager = signInManager;
			this.usersService = usersService;
			this.studentsRepository = studentsRepository;
			this.userManager = userManager;
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

		[Authorize]
		public IActionResult Logout()
		{
			signInManager.SignOutAsync().Wait();
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public IActionResult Profile(string userName)
		{
			var user = usersService.TryGetUserByName(userName);

			if (user == null)
				return NotFound();

			return View(user);
		}

		[Authorize]
		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}


		[Authorize]
		[HttpPost]
		public IActionResult ChangePassword(ChangePasswordViewModel model, string userName)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = usersService.TryGetUserByName(userName);
			
			if (user == null)
				return RedirectToAction("Login");

			var result = userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword).Result;
			if (result.Succeeded)
			{
				signInManager.RefreshSignInAsync(user).Wait();
				TempData["Success"] = "Пароль успешно изменён";
				return RedirectToAction("Profile", new {userName = User.Identity.Name});
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}


	}
}
