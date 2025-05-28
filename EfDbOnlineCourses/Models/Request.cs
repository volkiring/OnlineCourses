namespace EfDbOnlineCourses.Models
{
	public class Request
	{
		public int Id { get; set; }
		public User User { get; set; }
		public RequestType Type { get; set; }
		public string? Message { get; set; }
		public DateTime SubmittedAt { get; set; } = DateTime.Now;
		public bool Status { get; set; } = false;

	}
}
