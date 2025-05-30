using Microsoft.AspNetCore.Identity;

namespace EfDbOnlineCourses.Models
{
    public class User : IdentityUser
    {
        public DateTime? Birthdate { get; set; }
        public List<Course> Courses { get; set; } = [];
        public List<Request> Requests { get; set; } = [];

        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }
}
