using System;
using System.ComponentModel.DataAnnotations;

public class StudentViewModel
{
	[EmailAddress(ErrorMessage = "Некорректный email")]
	public string? Email { get; set; }

	[DataType(DataType.Date)]
	[Display(Name = "Дата рождения")]
	public DateTime? Birthdate { get; set; }

	[Required(ErrorMessage = "Логин обязателен")]
	public string UserName { get; set; }

	[Required(ErrorMessage = "Пароль обязателен")]
	[MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
}
