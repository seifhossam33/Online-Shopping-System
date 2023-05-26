using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingAdminService.Classes
{
    public class CartItems
    {
        private string itemId;
        private int quantity;
        private int cart_id;
        private Item itemDetails=new Item();

        public CartItems()
        {
        }

        public CartItems(string itemId, int quantity)
        {
            this.itemId = itemId;
            this.quantity = quantity;
        }

        public string ItemId { get => itemId; set => itemId = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Cart_id { get => cart_id; set => cart_id = value; }
        public Item ItemDetails { get => itemDetails; set => itemDetails = value; }
    }
}