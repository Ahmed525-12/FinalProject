using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOCore.Entites;

namespace ThriftinessCore.Entites
{
    public class Expense : BaseEntity
    {
        public int AmountMoney { get; set; }
        public bool Priority { get; set; }
        public string Title { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public MonthOfExpense MonthOfExpense { get; set; }

        [ForeignKey("MonthOfExpense")]
        public int MonthOfExpenseId { get; set; }
    }
}