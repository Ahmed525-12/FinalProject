using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemeberMe { get; set; }
    }
}