namespace DbWebApplication.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Register
	{
		[Required(ErrorMessage = "Имя пользователя обязательно")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 50 символов")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email обязателен")]
		[EmailAddress(ErrorMessage = "Некорректный email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Пароль обязателен")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Подтверждение пароля обязательно")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}

}