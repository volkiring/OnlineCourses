using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EfDbOnlineCourses.Models
{
	public class Request
	{
		public int Id { get; set; }
		public User User { get; set; }
		public RequestType Type { get; set; }
		public string? Message { get; set; }
		public Specialty Specialty { get; set; }
		public DateTime SubmittedAt { get; set; } 

		public RequestStatus Status { get; set; } = RequestStatus.Pending;
	}
}
