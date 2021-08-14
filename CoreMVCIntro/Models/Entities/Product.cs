using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class Product : BaseEnitity
    {
        public string ProductName { get; set; }
        public short UnitInStock { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }

        //Relational Properties
        public virtual Category Category { get; set; }
    }
}
