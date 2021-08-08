using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class Order:BaseEnitity
    {
        public int ShippedAddress { get; set; }
        public int EmployeeID { get; set; }

        //
        public virtual Employee Employee { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
