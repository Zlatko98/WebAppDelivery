using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppDelivery.Models.Classes
{
    public abstract class UserAb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        string userName;
        string email;
        string password;
        string name;
        string surname;
        DateTime birthDate;
        string address;
        //[ConcurrencyCheck]
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