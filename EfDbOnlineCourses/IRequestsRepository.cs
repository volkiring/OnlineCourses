using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IRequestsRepository
	{
		List<Request> GetAll();
		Request TryGetById(int Id);
		void Add(Request request);
		void Deny(Request request);
	}
}