using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
	public class ForgetPasswordVM
	{
		[Required(ErrorMessage = "Email is requird")]
		[EmailAddress(ErrorMessage = "Email invalid")]
		public string Email { get; set; }
	}
}