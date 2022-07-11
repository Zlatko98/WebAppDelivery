using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppDelivery.Models.Classes
{
    public abstract class UserAb
    {
        int id;
        string userName;
        string email;
        string password;
        string name;
        string surname;
        DateTime birthDate;
        string address;
        UserType userType;
        string image;

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Address { get => address; set => address = value; }
        public UserType UserType { get => userType; set => userType = value; }
        public string Image { get => image; set => image = value; }
    }
}