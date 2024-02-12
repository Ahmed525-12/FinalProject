using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
	public class MonthOfExpenseSpecf : BaseSpecfiction<MonthOfExpense>
	{
		public MonthOfExpenseSpecf(string userId) : base(p => p.User_Id == userId)
		{
			Includes.Add(p => p.Expenses);
		}
	}
}