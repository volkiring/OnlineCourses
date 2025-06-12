using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IModuleRepository
	{
		void AddLessonToModule(int moduleId, Lesson lesson);
		void AddModule(Module module, int courseId);
		void EditLesson(Lesson updatedLesson, int lessonId);
		void EditModule(Module updatedModule, Module module);
		void RemoveLesson(int lessonId);
		void RemoveModule(int moduleId);
		List<Module> TryGetByCourseId(int courseId);
		Module TryGetById(int moduleId);
		Lesson TryGetLessonById(int lessonId);
	}
}