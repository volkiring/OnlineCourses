using EfDbOnlineCourses.Models;

namespace EfDbOnlineCourses
{
	public class RequestsDbRepository : IRequestsRepository
	{
		private readonly DatabaseContext databaseContext;
		public RequestsDbRepository(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}
		public List<Request> GetAll()
		{
			return databaseContext.Requests.ToList();
		}

		public Request TryGetById(string Id)
		{
			return databaseContext.Requests.FirstOrDefault(x => x.Id == Id);
		}
	}
}
