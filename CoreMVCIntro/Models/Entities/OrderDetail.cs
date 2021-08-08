﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class OrderDetail:BaseEnitity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        //
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}