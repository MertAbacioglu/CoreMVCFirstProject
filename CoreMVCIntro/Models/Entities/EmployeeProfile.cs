using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class EmployeeProfile:BaseEnitity
    {
        public string SpecialDetails { get; set; }

        //Relational Properties
        public virtual Employee Employee { get; set; }
    }
}
