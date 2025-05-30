namespace EfDbOnlineCourses.Models
{

    public class Teacher
    {
        public string UserId { get; set; } // Первичный ключ + внешний ключ
        public User User { get; set; }     // Навигационное свойство

        public Specialty? Specialty { get; set; }
        public List<Course> CoursesTaught { get; set; } = new();
    }
}
