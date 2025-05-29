using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;

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
			return databaseContext.Requests.Include(r => r.Type).Include(r => r.Specialty).Include(r => r.User).ToList();
		}

		public Request TryGetById(int Id)
		{
			return databaseContext.Requests.Include(r => r.Type).Include(r => r.Specialty).Include(r => r.User).FirstOrDefault(x => x.Id == Id);
		}

		public void Add(Request request)
		{
			databaseContext.Requests.Add(request);
			databaseContext.SaveChanges();
		}

		public void Accept(Request request)
		{
			request.Status = RequestStatus.Accepted;
			databaseContext.SaveChanges();
		}
		public void Deny(Request request)
		{
			request.Status = RequestStatus.Denied;
			databaseContext.SaveChanges();
		}
	}
}
