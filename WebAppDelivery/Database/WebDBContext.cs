using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Database
{
    public class WebDBContext :DbContext
    {
        public WebDBContext() : base("WebDBContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new DelivererMap());

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
    }


    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            Property(x => x.UserName).IsRequired().HasMaxLength(450).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
        }
    }

    public class DelivererMap : EntityTypeConfiguration<Deliverer>
    {
        public DelivererMap()
        {
            Property(x => x.UserName).IsRequired().HasMaxLength(450).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
        }
    }
}