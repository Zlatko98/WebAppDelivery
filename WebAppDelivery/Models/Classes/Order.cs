using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppDelivery.Models.Classes
{
    public class Order
    {
        int id;
        private List<Product> products;

        public int Id { get => id; set => id = value; }
        public List<Product> Products { get => products; set => products = value; }
    }
}