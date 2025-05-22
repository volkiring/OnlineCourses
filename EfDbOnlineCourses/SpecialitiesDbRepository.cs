using EfDbOnlineCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class SpecialitiesDbRepository : ISpecialitiesRepository
	{
		private readonly DatabaseContext databaseContext;
		public SpecialitiesDbRepository(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}
		public void Add(Specialty specialty)
		{
			databaseContext.Specialties.Add(specialty);
			databaseContext.SaveChanges();
		}

		public void Delete(Specialty specialty)
		{
			databaseContext.Remove(specialty);
			databaseContext.SaveChanges();
		}

		public void Update(Specialty specialty, Specialty updatedSpecialty)
		{
			specialty.Name = updatedSpecialty.Name;
			databaseContext.SaveChanges();
		}

		public Specialty TryGetById(int id)
		{
			return databaseContext.Specialties.FirstOrDefault(x => x.Id == id);
		}

		public List<Specialty> GetAll()
		{
			return databaseContext.Specialties.ToList();
		}
	}
}
