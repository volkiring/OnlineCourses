using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDbOnlineCourses.Models
{
    public class Specialty
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Название обязательно")]
		public string Name { get; set; }
    }
}
