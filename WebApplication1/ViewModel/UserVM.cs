using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class UserVM
    {
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int? MonthlySalary { get; set; }
        public int? DayOfEndMonth { get; set; }
        public int? TotalExpense { get; set; }
    }
}