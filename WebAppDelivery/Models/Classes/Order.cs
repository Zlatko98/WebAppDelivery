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
        private List<int> amounts;
        private string address;
        private string comment;
        private double total;
        private OrderState orderState;
        public int Id { get => id; set => id = value; }
        public List<Product> Products { get => products; set => products = value; }
        public string Address { get => address; set => address = value; }
        public string Comment { get => comment; set => comment = value; }
        public double Total { get => total; set => total = value; }
        public List<int> Amounts { get => amounts; set => amounts = value; }
        public OrderState OrderState { get => orderState; set => orderState = value; }
    }
}