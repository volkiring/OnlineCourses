
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddDbContext<IdentityContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<IdentityContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.ExpireTimeSpan = TimeSpan.FromDays(31);
	options.LoginPath = "/Account/Login";
	options.LogoutPath = "/Account/Logout";
	options.Cookie = new CookieBuilder()
	{
		IsEssential = true
	};
});


builder.Services.AddScoped<ICoursesRepository, CoursesDbRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsDbRepository>();
builder.Services.AddScoped<ITeachersRepository, TeachersDbRepository>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
	var services = serviceScope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<User>>();
	var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	IdentityInitializer.Initialize(userManager, rolesManager);
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
