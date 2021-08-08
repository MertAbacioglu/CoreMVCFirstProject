using CoreMVCIntro.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Configuration
{
    public class OrderDetailConfiguration:BaseConfiguration<OrderDetail>
    {
        //Burada veri tabanı için ayarlama yapmak istiyoruz
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);    //Base'i olduğu gibi bırakıyoruz
            builder.Ignore(x => x.ID);
            builder.HasKey(x => new { x.OrderID, x.ProductID });
            builder.ToTable("Satıslar");
        }
    }
}
