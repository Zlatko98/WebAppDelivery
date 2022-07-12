using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Database
{
    public class Repository
    {
        WebDBContext webDBContext = new WebDBContext();


        public string addUser(User user)
        {
            try
            {
                webDBContext.Users.Add(user);
                webDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Successfully added.";
        }
    }
}