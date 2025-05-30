using EfDbOnlineCourses.Migrations;
using EfDbOnlineCourses.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EfDbOnlineCourses
{
    public class TeachersDbRepository : ITeachersRepository
    {
        private readonly DatabaseContext dbcontext;
        private readonly UserManager<User> userManager;
        public TeachersDbRepository(DatabaseContext dbContext, UserManager<User> userManager)
        {
            this.dbcontext = dbContext;
            this.userManager = userManager;
        }

        public List<Teacher> GetAll()
        {
            return dbcontext.Teachers.Include(t => t.User).Include(t => t.Specialty).ToList();
        }

        public void Add(User user, Specialty specialty)
        {
            user.Teacher = new Teacher()
            {
                UserId = user.Id,
                Specialty = specialty
            };

            dbcontext.SaveChanges();

            //if (teacher.PasswordHash == null)
            //{
            //	userManager.CreateAsync(teacher, password).Wait();
            //}
            //else
            //{
            //	userManager.CreateAsync(teacher).Wait();
            //}
        }

        public void Update(Teacher teacher, Teacher updatedTeacher)
        {
            //teacher.UserName = updatedTeacher.UserName;
            //teacher.Email = updatedTeacher.Email;
            //teacher.Birthdate = updatedTeacher.Birthdate;
            //teacher.Specialty = updatedTeacher.Specialty;

            //userManager.UpdateAsync(teacher).Wait();
            //dbcontext.SaveChanges();
        }
        public void Delete(Teacher teacher)
        {
            //userManager.DeleteAsync(teacher).Wait();
        }

        public Teacher TryGetById(string id)
        {
            return dbcontext.Teachers.Include(c => c.User).Include(t => t.Specialty).FirstOrDefault(t => t.UserId == id);
        }

    }
}
