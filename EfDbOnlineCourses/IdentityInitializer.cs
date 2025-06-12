using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;

public class IdentityInitializer
{
	public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IStudentsRepository studentsRepository)
	{
		var adminEmail = "admin@gmail.com";
		var password = "z!PZ\"^4GuYecP5B";

		if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
		{
			roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
		}

		if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
		{
			roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
		}

		if (roleManager.FindByNameAsync(Constants.TeacherRoleName).Result == null)
		{
			roleManager.CreateAsync(new IdentityRole(Constants.TeacherRoleName)).Wait();
		}


		if (userManager.FindByNameAsync(adminEmail).Result == null)
		{
			var admin = new User
			{
				Email = adminEmail,
				UserName = adminEmail
			};
			var result = studentsRepository.Add(admin, password, out var createdUser);

			if (result.Succeeded)
			{
				userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();

			}
		}
	}

}