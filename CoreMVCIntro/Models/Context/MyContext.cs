using CoreMVCIntro.Configuration;
using CoreMVCIntro.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CoreMVCIntro.Models.Context
{
    //EFSqlServer kütüphanesini indirmeliyiz. Options ayarlamalarını yapabilmekk için gereklidir.
    public class MyContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #region Yontem1
        //    optionsBuilder.UseSqlServer("connectionString=database=CoreMVCIntro;provideName=System.Data.SqlClient;integrated security=true");
        //    #endregion





        //}

        //Biz şöyle yapalım : 

        //Dependecy Injection yapısı core platformumuzun arkasında otomatik olarak entegre gelir. Database  sınıfının cTor'una parametre olarak bir Options tipinde verirsek bu parammetreye argüman otomatik gönderilir. 
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
            //DbContextOptions<MyContext> options ---> Pooldaki yani. o da kendi base'ine gödneriyor
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());//n-n ilişki ayarları barındır
            base.OnModelCreating(modelBuilder);//1-1 ilişki ayarları barındırır

        }
        // .NetCore üzerinden migrate yapmak istediğimiz taktirde add-migration <parametre> ve sonfrasında update-database demek gerek

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfile { get; set; }

    }
}
