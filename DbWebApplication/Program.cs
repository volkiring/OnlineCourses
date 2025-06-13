
using DbWebApplication;
using EfDbOnlineCourses;
using EfDbOnlineCourses.Models;
using EfDbOnlineCourses.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<DatabaseContext>();

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

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Events.OnValidatePrincipal = async context =>
	{
		var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
		var user = await userManager.GetUserAsync(context.Principal);

		if (user == null)
		{
			context.RejectPrincipal();
			await context.HttpContext.SignOutAsync();
		}
	};
});



builder.Services.AddScoped<ICoursesRepository, CoursesDbRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsDbRepository>();
builder.Services.AddScoped<ITeachersRepository, TeachersDbRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ISpecialitiesRepository, SpecialitiesDbRepository>();	
builder.Services.AddScoped<IRequestsRepository, RequestsDbRepository>();
builder.Services.AddScoped<IRequestTypeRepository, RequestTypeDbRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleDbRepository>();	
builder.Services.AddScoped<ILessonRepository, LessonDbRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewDbRepository>();

var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
	var services = serviceScope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<User>>();
	var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	var studentManager = services.GetRequiredService<IStudentsRepository>();
	var context = services.GetRequiredService<DatabaseContext>();
	IdentityInitializer.Initialize(userManager, rolesManager, studentManager);
	DbSeeder.SeedDatabase(context);
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
	name: "Admin",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();



app.Run();
