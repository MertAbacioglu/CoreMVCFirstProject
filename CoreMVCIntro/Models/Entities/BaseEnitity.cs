using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public abstract class BaseEnitity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
