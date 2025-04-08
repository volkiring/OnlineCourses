using EfDbPolyclinic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;

public class ApplicationDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Lesson> lessons { get; set; }	
	public DbSet<Teacher> teachers { get; set; }
	public DbSet<Grade> grades { get; set; }



	public ApplicationDbContext()
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = "Server=localhost;Database=onlinecourses;Uid=root;Pwd=99977788866aaaAa;";
		optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
	}
}


public class Program
{
	public static void Main()
	{
		using ApplicationDbContext context = new();
		context.SaveChanges();

	}
}