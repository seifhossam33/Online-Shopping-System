using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;


namespace OnlineShoppingAdminService.Classes
{
    public class User
    {
        private string name, email, password, address, area, mobileNumber;

        private int cart_id;
        public List<CartItems> cart = new List<CartItems>();

        public User(string name, string email, string password, string address, string area, string mobileNumber, int cart_id, List<CartItems> cart)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.address = address;
            this.area = area;
            this.mobileNumber = mobileNumber;
            this.cart_id = cart_id;
            this.cart = cart;
        }

        public User()
        {
        }

        public string Name { get => Name1; set => Name1 = value; }
        public string Email { get => Email1; set => Email1 = value; }
        public string Password { get => Password1; set => Password1 = value; }
        public string Address { get => Address1; set => Address1 = value; }
        public string Area { get => Area1; set => Area1 = value; }
        public string MobileNumber { get => MobileNumber1; set => MobileNumber1 = value; }
        public string Name1 { get => name; set => name = value; }
        public string Email1 { get => email; set => email = value; }
        public string Password1 { get => password; set => password = value; }
        public string Address1 { get => address; set => address = value; }
        public string Area1 { get => area; set => area = value; }
        public string MobileNumber1 { get => mobileNumber; set => mobileNumber = value; }
        public int Cart_id { get => cart_id; set => cart_id = value; }
        // public Dictionary<string, int> Cart { get => cart; set => cart = value; }
    }
}