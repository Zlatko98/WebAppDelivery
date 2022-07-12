using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Database
{
    public class WebDBContext :DbContext
    {
        public WebDBContext() : base("WebDBContext") { }


        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
    }
}