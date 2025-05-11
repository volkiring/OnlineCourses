﻿using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;

public class IdentityInitializer
{
	public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
	{
		var adminEmail = "admin@gmail.com";
		var password = "123456";

		if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
		{
			roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
		}

		if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
		{
			roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
		}

		if (userManager.FindByNameAsync(adminEmail).Result == null)
		{
			var admin = new User
			{
				Email = adminEmail,
				UserName = adminEmail
			};
			var result = userManager.CreateAsync(admin).Result;
			if (result.Succeeded)
			{
				userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();
			}
		}
	}

}