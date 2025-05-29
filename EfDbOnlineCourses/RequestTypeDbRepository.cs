using EfDbOnlineCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses
{
	public class RequestTypeDbRepository : IRequestTypeRepository
	{
		private readonly DatabaseContext databaseContext;
		public RequestTypeDbRepository(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}
		public List<RequestType> GetAll()
		{
			return databaseContext.RequestTypes.ToList();
		}

		public RequestType TryGetById(int id)
		{
			return databaseContext.RequestTypes.FirstOrDefault(x => x.Id == id);
		}
	}
}
