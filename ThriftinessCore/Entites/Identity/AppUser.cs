using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftinessCore.Entites.Identity
{
    public class AppUser : IdentityUser
    {
        public int? MonthlySalary { get; set; }
        public int? DayOfEndMonth { get; set; }
        public int? TotalExpense { get; set; }
        public string DisplayName { get; set; }
    }
}