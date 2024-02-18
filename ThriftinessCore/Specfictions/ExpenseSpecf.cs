using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
    public class ExpenseSpecf : BaseSpecfiction<Expense>
    {
        public ExpenseSpecf(string userId) : base(p => p.UserId == userId)
        {
        }
    }
}