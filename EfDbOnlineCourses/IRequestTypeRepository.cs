using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface IRequestTypeRepository
	{
		List<RequestType> GetAll();
		RequestType TryGetById(int id);
	}
}