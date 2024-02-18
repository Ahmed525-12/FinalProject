using System.ComponentModel.DataAnnotations.Schema;
using ThriftinessCore.Entites;

namespace WebApplication1.ViewModel
{
    public class ExpenseVM
    {
        public int Id { get; set; }
        public int AmountMoney { get; set; }
        public bool Priority { get; set; }
        public string Title { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }

        public int MonthOfExpenseId { get; set; }
    }
}