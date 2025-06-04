using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IModuleService
	{
		void AddLessonToModule(int moduleId, Lesson lesson);
		void AddModule(Module module, int courseId);
		void RemoveLesson(int lessonId);
		List<Module> TryGetByCourseId(int courseId);
		Module TryGetById(int courseId);
	}
}