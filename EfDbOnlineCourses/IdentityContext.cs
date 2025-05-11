using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses
{
	public class IdentityContext : IdentityDbContext<User>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options) : base (options)
		{
			Database.Migrate();
		}
	}
}
 