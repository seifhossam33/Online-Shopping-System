using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingAdminService.Classes
{
    public class Item
    {
        private string id;
        private string name;
        private string description;
        private string category;
        private int unit_price , quantity;
        private string imagePath;


        public Item()
        {
        }

        public Item(string id, string name, string description, string category, int unit_price, int quantity, string imagePath)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.category = category;
            this.unit_price = unit_price;
            this.quantity = quantity;
            this.ImagePath = imagePath;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Category { get => category; set => category = value; }
        public int Unit_price { get => unit_price; set => unit_price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }

        public void setId(string id)
        {
            this.Id = id;
        }
        public void setName(string name)
        {
            this.Name = name;
        }
        public void setDescription(string description)
        {
            this.Description = description;
        }

        public void setCategory(string category)
        {
            this.Category = category;
        }

        public void setPrice(int price)
        {
            this.Unit_price = price;
        }
    }
}