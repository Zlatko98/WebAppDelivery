using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppDelivery.Models.Classes
{
    public class User : UserAb
    {
        List<Order> orders;

        public List<Order> Orders { get => orders; set => orders = value; }
    }
}