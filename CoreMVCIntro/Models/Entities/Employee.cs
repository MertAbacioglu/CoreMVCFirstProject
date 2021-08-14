using CoreMVCIntro.Models.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class Employee : BaseEnitity
    {
        public string FirstName { get; set; }
        public UserRole UserRole { get; set; }

        //
        public virtual IList<Order> Orders { get; set; }
        public virtual EmployeeProfile GetEmployeeProfile { get; set; }

    }
}
