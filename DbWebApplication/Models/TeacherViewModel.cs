namespace DbWebApplication.Models
{
	using EfDbOnlineCourses.Models;
	using System;
	using System.ComponentModel.DataAnnotations;

	public class TeacherViewModel
	{
		[Required(ErrorMessage = "Имя обязательно")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Специальность обязательна")]
		public int specialtyId { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		[CustomValidation(typeof(TeacherViewModel), nameof(ValidateBirthdate))]
		public DateTime? Birthdate { get; set; }

		[Required(ErrorMessage = "Email обязателен")]
		[EmailAddress(ErrorMessage = "Некорректный формат email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Пароль обязателен")]
		[MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public static ValidationResult ValidateBirthdate(DateTime? birthdate, ValidationContext context)
		{
			if (birthdate.HasValue && birthdate.Value > DateTime.Today)
			{
				return new ValidationResult("Дата рождения не может быть в будущем");
			}
			return ValidationResult.Success;
		}
	}

}
