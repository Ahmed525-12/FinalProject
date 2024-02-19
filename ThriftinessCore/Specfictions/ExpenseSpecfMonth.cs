using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
    public class ExpenseSpecfMonth : BaseSpecfiction<Expense>
    {
        public ExpenseSpecfMonth(int? monthId) : base(p => p.MonthOfExpenseId == monthId)
        {
        }
    }
}