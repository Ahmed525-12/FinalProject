using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftinessCore.Entites
{
    public class MonthOfExpense : BaseEntity
    {
        public MonthOfExpense()
        {
            Expenses = new HashSet<Expense>();
        }

        public int numOfMonth { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public int TotalAmountMoney { get; set; }
        public string User_Id { get; set; }
    }
}