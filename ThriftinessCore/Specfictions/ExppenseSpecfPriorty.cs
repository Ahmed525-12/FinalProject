using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
    public class ExppenseSpecfPriorty : BaseSpecfiction<Expense>
    {
        public ExppenseSpecfPriorty(bool? priorty, string userId) : base(p => (p.Priority == priorty) && (p.UserId == userId))
        {
        }
    }
}