using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppDelivery.Models.Classes
{
    public class Product
    {
        string id;
        private string name;
        private string ingredients;
        private double price;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Ingredients { get => ingredients; set => ingredients = value; }
        public double Price { get => price; set => price = value; }
    }
}