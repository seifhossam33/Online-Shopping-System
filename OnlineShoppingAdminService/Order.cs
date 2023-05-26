using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingAdminService
{
    public class Order
    {
        string id;
        DateTime date;
        int totalPrice;

        public Order()
        {

        }

        public int TotalPrice { get => totalPrice; set => totalPrice = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Id { get => id; set => id = value; }
    }
}