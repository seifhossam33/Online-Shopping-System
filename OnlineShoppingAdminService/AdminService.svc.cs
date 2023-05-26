using OnlineShoppingAdminService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace OnlineShoppingAdminService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AdminService.svc or AdminService.svc.cs at the Solution Explorer and start debugging.

    public class AdminService : IAdminService
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=OnlineShoppingdb;Integrated Security=True");

        void IAdminService.AddItem(Item item)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into Item(id,name,description,category,unit_price,quantity,imagePath) values (@id,@name,@description, @category, @price, @quantity, @imagePath)", con);

            SqlParameter itemId = new SqlParameter("@id", item.Id);
            SqlParameter itemName = new SqlParameter("@name", item.Name);
            SqlParameter itemDescription = new SqlParameter("@description", item.Description);
            SqlParameter itemCategory = new SqlParameter("@category", item.Category);
            SqlParameter itemPrice = new SqlParameter("@price", item.Unit_price);
            SqlParameter itemQuantity = new SqlParameter("@quantity", item.Quantity);
            SqlParameter itemImagePath = new SqlParameter("@imagePath", item.ImagePath);

            cmd.Parameters.Add(itemId);
            cmd.Parameters.Add(itemName);
            cmd.Parameters.Add(itemDescription);
            cmd.Parameters.Add(itemCategory);
            cmd.Parameters.Add(itemPrice);
            cmd.Parameters.Add(itemQuantity);
            cmd.Parameters.Add(itemImagePath);

            cmd.ExecuteNonQuery();

            con.Close();
        }

        List<String> IAdminService.RetrieveID()
        {
            List<String> ids = new List<String>();

            con.Open();

            SqlCommand cmd = new SqlCommand("select id from Item", con);
            SqlDataReader reader = cmd.ExecuteReader();

            string id;

            while (reader.Read())
            {
                id = reader["id"].ToString();
                ids.Add(id);
            }

            reader.Close();

            con.Close();

            return ids;
        }

        void IAdminService.DeleteItem(string id)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("delete from Item where id=@id", con);
            SqlParameter itemId = new SqlParameter("@id", id);
            cmd.Parameters.Add(itemId);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        Item IAdminService.RetriveItemToEdit(string id)
        {
             if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("The id parameter is null or empty.");
            }

            con.Open();

            Item tmp = null;

            SqlCommand cmd = new SqlCommand("select * from Item where id=@id", con);
            SqlParameter itemId = new SqlParameter("@id", id);

            cmd.Parameters.Add(itemId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                tmp = new Item();
                tmp.Id = reader["id"].ToString();
                tmp.Name = reader["name"].ToString();
                tmp.Description = reader["description"].ToString();
                tmp.Category = reader["category"].ToString();
                tmp.Unit_price = Convert.ToInt32(reader["unit_price"]);
                tmp.Quantity = Convert.ToInt32(reader["quantity"]);
                tmp.ImagePath = reader["imagePath"].ToString();
            }

            reader.Close();

            con.Close();

            return tmp;

        }

        void IAdminService.EditItem(Item tmp)
        {
           con.Open();

            SqlCommand cmd = new SqlCommand("update Item SET id=@id, name=@name, description=@description, category=@category, unit_price=@price, quantity=@quantity, imagePath=@ImagePath where id=@id", con);
            SqlParameter itemId = new SqlParameter("@id", tmp.Id);
            SqlParameter itemName = new SqlParameter("@name", tmp.Name);
            SqlParameter itemDescription = new SqlParameter("@description", tmp.Description);
            SqlParameter itemCategory = new SqlParameter("@category", tmp.Category);
            SqlParameter itemPrice = new SqlParameter("@price", tmp.Unit_price);
            SqlParameter itemQuantity = new SqlParameter("@quantity", tmp.Quantity);
            SqlParameter itemImagePath = new SqlParameter("@ImagePath", tmp.ImagePath);

            cmd.Parameters.Add(itemId);
            cmd.Parameters.Add(itemName);
            cmd.Parameters.Add(itemDescription);
            cmd.Parameters.Add(itemCategory);
            cmd.Parameters.Add(itemPrice);
            cmd.Parameters.Add(itemImagePath);
            cmd.Parameters.Add(itemQuantity);

            cmd.ExecuteNonQuery();

            con.Close();

        }

        List<Item> IAdminService.ViewItems()
        {
            List<Item> items = new List<Item>();

            con.Open();

            SqlCommand cmd = new SqlCommand("select * from Item", con);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while(reader.Read())
            {

                Item item = new Item();
                item.Id = reader["id"].ToString();
                item.Name = reader["name"].ToString();
                item.Description = reader["description"].ToString();
                item.Category = reader["category"].ToString();
                item.Unit_price = Convert.ToInt32(reader["unit_price"]);
                item.Quantity = Convert.ToInt32(reader["quantity"]);
                item.ImagePath = reader["imagePath"].ToString();

                items.Add(item);
            }

            reader.Close();

            con.Close();

            return items;
        }
    }
}