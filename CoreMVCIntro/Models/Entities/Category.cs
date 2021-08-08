using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Models.Entities
{
    public class Category:BaseEnitity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        //
        public List<Product> Products { get; set; }
    }
}
