namespace EfDbOnlineCourses.Models
{
    public class Student
    {
        public string UserId { get; set; } // Первичный ключ + внешний ключ
        public User User { get; set; }     // Навигационное свойство
    }
}
