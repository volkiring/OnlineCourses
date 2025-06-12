using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ILessonRepository
	{
		void AddLessonToModule(int moduleId, Lesson lesson);
		void EditLesson(Lesson updatedLesson, int lessonId);
		void RemoveLesson(int lessonId);
		Lesson TryGetLessonById(int lessonId);
		List<Lesson> TryGetLessonsByModuleId(int moduleId);
	}
}