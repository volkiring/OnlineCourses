using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IReviewRepository
	{
		void Add(Course course, Student student, Review review);
		List<Review> TryGetByCourseId(int courseId);
	}
}