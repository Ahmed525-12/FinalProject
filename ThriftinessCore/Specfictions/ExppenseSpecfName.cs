using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
    public class ExppenseSpecfName : BaseSpecfiction<Expense>
    {
        public ExppenseSpecfName(string searchValue, string userId) : base(p => (p.Title.ToLower().Contains(searchValue.ToLower())) && (p.UserId == userId))
        {
        }
    }
}