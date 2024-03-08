using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "name is requird")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is requird")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password dosent match")]
        public string ConfirmPassword { get; set; }
    }
}