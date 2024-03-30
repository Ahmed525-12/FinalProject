using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class UserSettingsVM
    {
        [Required(ErrorMessage = "MonthlySalary is required")]
        public int? MonthlySalary { get; set; }

        [Required(ErrorMessage = "MonthlySalary is required")]
        public int? DayOfEndMonth { get; set; }
    }
}