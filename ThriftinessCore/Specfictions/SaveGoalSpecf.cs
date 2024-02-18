using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftinessCore.Entites;

namespace ThriftinessCore.Specfictions
{
    public class SaveGoalSpecf : BaseSpecfiction<SaveGoal>
    {
        public SaveGoalSpecf(string userId) : base(p => p.User_Id == userId)
        {
        }
    }
}