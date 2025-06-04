using System.ComponentModel.DataAnnotations;

public class ChangePasswordViewModel
{
	[Required]
	[DataType(DataType.Password)]
	[Display(Name = "Текущий пароль")]
	public string CurrentPassword { get; set; }

	[Required]
	[StringLength(100, MinimumLength = 6)]
	[DataType(DataType.Password)]
	[Display(Name = "Новый пароль")]
	public string NewPassword { get; set; }

	[DataType(DataType.Password)]
	[Display(Name = "Подтверждение нового пароля")]
	[Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
	public string ConfirmPassword { get; set; }
}
