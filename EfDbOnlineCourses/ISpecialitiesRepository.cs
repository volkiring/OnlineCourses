using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public interface ISpecialitiesRepository
	{
		void Add(Specialty specialty);
		List<Specialty> GetAll();
		Specialty TryGetById(int id);
		void Update(Specialty specialty, Specialty updatedSpecialty);
	}
}