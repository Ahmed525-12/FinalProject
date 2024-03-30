using System.ComponentModel.DataAnnotations;
using ThriftinessCore.Entites;

namespace WebApplication1.ViewModel
{
    public class ExpenseVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount of money is required")]
        public int AmountMoney { get; set; }

        public bool Priority { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }

        public int MonthOfExpenseId { get; set; }
    }
}