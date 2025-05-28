using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IRequestsRepository
	{
		List<Request> GetAll();
		Request TryGetById(string Id);
	}
}