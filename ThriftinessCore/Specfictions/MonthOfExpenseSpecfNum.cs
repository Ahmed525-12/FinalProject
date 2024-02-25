using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
    public class MonthOfExpenseSpecfNum : BaseSpecfiction<MonthOfExpense>
    {
        public MonthOfExpenseSpecfNum(int numofmonth, string userId) : base(p => (p.numOfMonth == numofmonth) && (p.User_Id == userId))
        {
        }
    }
}