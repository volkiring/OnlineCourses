namespace DbWebApplication.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Register
	{
		[Required]
		[Display(Name = "Имя пользователя")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Электронная почта")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(6)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[Display(Name = "Подтверждение пароля")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "Дата рождения")]
		[DataType(DataType.Date)]
		public DateTime? Birthdate { get; set; }
	}

}