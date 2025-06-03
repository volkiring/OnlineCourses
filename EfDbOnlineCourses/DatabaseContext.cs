using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Teacher>()
	.HasKey(t => t.UserId);

			modelBuilder.Entity<Teacher>()
				.HasOne(t => t.User)
				.WithOne(u => u.Teacher)
				.HasForeignKey<Teacher>(t => t.UserId);

			modelBuilder.Entity<Student>()
				.HasKey(s => s.UserId);

			modelBuilder.Entity<Student>()
				.HasOne(s => s.User)
				.WithOne(u => u.Student)
				.HasForeignKey<Student>(s => s.UserId);


			modelBuilder.Entity<RequestType>().HasData(
            new RequestType()
            {
                Id = 1,
                Name = "Заявка на становление преподавателем"
            },
            new RequestType()
            {
                Id = 2,
                Name = "Заявка на становление администратором"
            }
            );

            modelBuilder.Entity<Specialty>().HasData(
            new Specialty { Id = 1, Name = "Математика" },
            new Specialty { Id = 2, Name = "Программирование" }
            );

            modelBuilder.Entity<Request>()
    .Property(r => r.Status)
    .HasConversion<string>();

        }

    }
}
