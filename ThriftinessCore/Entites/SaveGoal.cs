using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOCore.Entites;

namespace ThriftinessCore.Entites
{
    public class SaveGoal : BaseEntity
    {
        public string TitleGoal { get; set; }
        public int TargetAmount { get; set; }
        public string User_Id { get; set; }
    }
}