using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppDelivery.Models.Classes
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        private string name;
        private string ingredients;
        private double price;
        private List<Order> orders;

        
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Ingredients { get => ingredients; set => ingredients = value; }
        public double Price { get => price; set => price = value; }
        public List<Order> Orders { get => orders; set => orders = value; }
    }
}