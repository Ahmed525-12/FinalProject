using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
	public class ResetPasswordVM
	{
		[Required(ErrorMessage = "password is requird")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is requird")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "dosent match")]
		public string ConfirmPassword { get; set; }
	}
}