using EfDbOnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Repositories
{
	public class ReviewDbRepository : IReviewRepository
	{
		private readonly DatabaseContext databaseContext;
		private readonly IStudentsRepository studentsRepository;
		private readonly ICoursesRepository coursesRepository;

		public ReviewDbRepository(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public void Add(Course course, Student student, Review review)
		{
			review.Course = course;
			review.Student = student;

			databaseContext.Reviews.Add(review);
			databaseContext.SaveChanges();
		}

		public List<Review> TryGetByCourseId(int courseId)
		{
			return databaseContext.Reviews.Include(r => r.Course).Include(r => r.Student).ThenInclude(s => s.User).ToList() ?? new List<Review>();
		}

	}
}
