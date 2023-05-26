using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using OnlineShoppingAdminService.Classes;

namespace OnlineShoppingAdminService
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=OnlineShoppingdb;Integrated Security=True");
        static User loggedUser = new User();
        [WebMethod]
        public bool registerUser(User enteredUser)
        {
            Console.WriteLine(enteredUser.Email);
            conn.Open();

            SqlCommand myCmd = new SqlCommand("select email from [User] where email=@tmpEmail", conn);
            SqlParameter p0 = new SqlParameter("@tmpEmail", enteredUser.Email);
            myCmd.Parameters.Add(p0);

            SqlDataReader reader = myCmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                conn.Close();
                conn.Open();
                SqlCommand insertCmd = new SqlCommand("insert into [User] (email,name,password,address,area,mobile) values (@tmpEmail,@tmpName,@tmpPassword,@tmpAddress,@tmpArea,@tmpMobile)", conn);
                SqlParameter p1 = new SqlParameter("@tmpEmail", enteredUser.Email);
                SqlParameter p2 = new SqlParameter("@tmpName", enteredUser.Name);
                SqlParameter p3 = new SqlParameter("@tmpPassword", enteredUser.Password);
                SqlParameter p4 = new SqlParameter("@tmpAddress", enteredUser.Address);
                SqlParameter p5 = new SqlParameter("@tmpArea", enteredUser.Area);
                SqlParameter p6 = new SqlParameter("@tmpMobile", enteredUser.MobileNumber);
                insertCmd.Parameters.Add(p1);
                insertCmd.Parameters.Add(p2);
                insertCmd.Parameters.Add(p3);
                insertCmd.Parameters.Add(p4);
                insertCmd.Parameters.Add(p5);
                insertCmd.Parameters.Add(p6);
                insertCmd.ExecuteNonQuery();
            }
            else
            {
                reader.Close();

                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }
        [WebMethod]
        public bool login(String email, String password)
        {
            conn.Open();

            SqlCommand myCmd = new SqlCommand("select * from [User] where email=@tmpEmail and password=@tmpPassword", conn);
            SqlParameter p0 = new SqlParameter("@tmpEmail", email);
            SqlParameter p1 = new SqlParameter("@tmpPassword", password);
            myCmd.Parameters.Add(p0);
            myCmd.Parameters.Add(p1);


            SqlDataReader reader = myCmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                conn.Close();
                return true;

            }
            reader.Close();
            conn.Close();
            return false;
        }
        [WebMethod]

        public User loadUserInfo(string email)
        {

            conn.Open();

            SqlCommand myCmd = new SqlCommand("select * from [User] where email=@tmpEmail", conn);
            SqlParameter p0 = new SqlParameter("@tmpEmail", email);
            myCmd.Parameters.Add(p0);

            SqlDataReader reader = myCmd.ExecuteReader();
            if (reader.Read()) {
                loggedUser.Email = email;
                loggedUser.Name = reader["name"].ToString();
                loggedUser.MobileNumber = reader["mobile"].ToString();
                loggedUser.Area = reader["area"].ToString();
                loggedUser.Address = reader["address"].ToString();
                loggedUser.Password = reader["password"].ToString();
                loggedUser.Cart_id = Convert.ToInt32(reader["cart_id"]);
                reader.Close();
                conn.Close();
            }
            System.Diagnostics.Debug.WriteLine("testing cart " + loggedUser.Name);
            return loggedUser;
        }
        [WebMethod]

        public User getUserInfo()
        {
            System.Diagnostics.Debug.WriteLine("get info " + loggedUser.Name.Trim() + loggedUser.Email.Trim());
            return loggedUser;
        }
        [WebMethod]

        public List<Item> ShowAvailableItems()
        {
            List<Item> items = new List<Item>();
            conn.Open();

            SqlCommand myCmd = new SqlCommand("select * from [Item] where quantity<>0", conn);
            SqlDataReader reader = myCmd.ExecuteReader();
            while (reader.Read())
            {
                Item i = new Item();
                i.Id = reader["id"].ToString();
                i.Name = reader["name"].ToString(); ;
                i.Description = reader["description"].ToString();
                i.Category = reader["category"].ToString();
                i.Unit_price = Convert.ToInt32(reader["unit_price"]);
                i.Quantity = Convert.ToInt32(reader["quantity"]);
                i.ImagePath = reader["imagePath"].ToString();

                items.Add(i);
            }

            reader.Close();
            conn.Close();

            return items;
        }
        [WebMethod]

        public void addToCart(string id, int quantity)
        {
            bool exists = false;
            conn.Open();
            System.Diagnostics.Debug.WriteLine("testing name " + loggedUser.Cart_id);
            SqlCommand selectCmd = new SqlCommand("select * from [UserCart] where cart_id=@cart and item_id=@item", conn);

            SqlParameter p0 = new SqlParameter("@item", id);
            SqlParameter p1 = new SqlParameter("@cart", loggedUser.Cart_id);
            selectCmd.Parameters.Add(p0);
            selectCmd.Parameters.Add(p1);
            SqlDataReader reader = selectCmd.ExecuteReader();

            if (reader.HasRows)
            {
                exists = true;
                reader.Close();
                conn.Close();

            }
            else {
                reader.Close();
                conn.Close();
            }
            if (exists)
            {
                conn.Open();
                SqlCommand updateCmd = new SqlCommand("UPDATE UserCart SET quantity = quantity+ @newQ WHERE cart_id = @cart and item_id=@item", conn);

                SqlParameter p5 = new SqlParameter("@item", id);
                SqlParameter p6 = new SqlParameter("@cart", loggedUser.Cart_id);
                SqlParameter p7 = new SqlParameter("@newQ", quantity);

                updateCmd.Parameters.Add(p5);
                updateCmd.Parameters.Add(p6);
                updateCmd.Parameters.Add(p7);
                updateCmd.ExecuteNonQuery();
                conn.Close();
                return;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                conn.Open();
                SqlCommand insertCmd = new SqlCommand("insert into [UserCart] (item_id,quantity,cart_id) values (@item,@quantity,@id)", conn);

                SqlParameter p2 = new SqlParameter("@item", id);
                SqlParameter p3 = new SqlParameter("@quantity", quantity);
                SqlParameter p4 = new SqlParameter("@id", loggedUser.Cart_id);

                insertCmd.Parameters.Add(p2);
                insertCmd.Parameters.Add(p3);
                insertCmd.Parameters.Add(p4);
                insertCmd.ExecuteNonQuery();
                conn.Close();
            }

            System.Diagnostics.Debug.WriteLine("testing cart size " + loggedUser.cart.Count());



        }
        [WebMethod]
        public List<CartItems> getCartInfo()
        {
            System.Diagnostics.Debug.WriteLine("testing cart " + loggedUser.Cart_id);
            conn.Open();
            SqlCommand myCmd = new SqlCommand("select UserCart.quantity, Item.Name, Item.id , Item.description , Item.category , Item.unit_price  from [UserCart] inner join Item on UserCart.item_id = Item.id and UserCart.cart_id = @cart", conn);
            SqlParameter p0 = new SqlParameter("@cart", loggedUser.Cart_id);
            myCmd.Parameters.Add(p0);

            SqlDataReader reader = myCmd.ExecuteReader();
            System.Diagnostics.Debug.WriteLine("testing reader " + reader.HasRows);
            loggedUser.cart.Clear();
            while (reader.Read())
            {
                System.Diagnostics.Debug.WriteLine("testing loop " + loggedUser.cart.Count());
                CartItems c = new CartItems();
                c.ItemId = reader["id"].ToString();
                c.Quantity = Convert.ToInt32(reader["quantity"]);
                c.Cart_id = loggedUser.Cart_id;
                c.ItemDetails.Description = reader["description"].ToString();
                c.ItemDetails.Unit_price = Convert.ToInt32(reader["unit_price"]);
                c.ItemDetails.Category = reader["category"].ToString();
                c.ItemDetails.Name = reader["name"].ToString();
                loggedUser.cart.Add(c);
                // System.Diagnostics.Debug.WriteLine("testing om el zeft " + reader["description"]);
            }
            reader.Close();
            conn.Close();
            System.Diagnostics.Debug.WriteLine("testing cart size 2 " + loggedUser.cart.Count());
            return loggedUser.cart;

        }

        [WebMethod]
        public void removeFromCart(string id)
        {
            for (int i = loggedUser.cart.Count - 1; i >= 0; i--)
            {
                CartItems c = loggedUser.cart[i];
                if (c.ItemId == id)
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand("delete from UserCart where cart_id = @x and item_id=@y", conn);

                    SqlParameter p2 = new SqlParameter("@x", loggedUser.Cart_id);
                    SqlParameter p1 = new SqlParameter("@y", id);

                    insertCmd.Parameters.Add(p1);
                    insertCmd.Parameters.Add(p2);
                    insertCmd.ExecuteNonQuery();

                    loggedUser.cart.RemoveAt(i);
                    conn.Close();
                }
            }
        }

        [WebMethod]
        public void updateInfo(User user)
        {
            conn.Open();
            SqlCommand UpdateCmd = new SqlCommand("update [User] Set name=@name , password=@password , address=@address , mobile=@mobile  where email = @email", conn);

            SqlParameter p1 = new SqlParameter("@name", user.Name);
            SqlParameter p2 = new SqlParameter("@password", user.Password);
            SqlParameter p3 = new SqlParameter("@address", user.Address);
            SqlParameter p4 = new SqlParameter("@mobile", user.MobileNumber);
            SqlParameter p5 = new SqlParameter("@email", loggedUser.Email);

            UpdateCmd.Parameters.Add(p1);
            UpdateCmd.Parameters.Add(p2);
            UpdateCmd.Parameters.Add(p3);
            UpdateCmd.Parameters.Add(p4);
            UpdateCmd.Parameters.Add(p5);

            UpdateCmd.ExecuteNonQuery();

            conn.Close();
        }
        [WebMethod]
        public void orderPlacement(int totalPrice)
        {
            getCartInfo();
            conn.Open();
            
            System.Diagnostics.Debug.WriteLine("testing om el zeft " + loggedUser.cart.Count);
            // Insert new order
            List<string> itemsToRemove = new List<string>();

            SqlCommand command = new SqlCommand("INSERT INTO [Order] (user_id, date,total_price) VALUES (@user_id, @date,@price); SELECT SCOPE_IDENTITY();", conn);

            command.Parameters.AddWithValue("@user_id", loggedUser.Email);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@price", totalPrice);

                // Get the order_id of the newly created order
            int order_id = Convert.ToInt32(command.ExecuteScalar());

            foreach (CartItems c in loggedUser.cart)
            {
                // Insert order items
                SqlCommand command2 = new SqlCommand("INSERT INTO Order_Items (order_id, item_id, quantity, unit_price) VALUES (@order_id, @item_id, @quantity, @unit_price)", conn);

                command2.Parameters.AddWithValue("@order_id", order_id);
                command2.Parameters.AddWithValue("@item_id", c.ItemId);
                command2.Parameters.AddWithValue("@quantity", c.Quantity);
                command2.Parameters.AddWithValue("@unit_price", c.ItemDetails.Unit_price);
                command2.ExecuteNonQuery();

                itemsToRemove.Add(c.ItemId);
            }
                conn.Close();

            foreach (string itemId in itemsToRemove)
            {
                removeFromCart(itemId);
            }
        }
        [WebMethod]
        public List<Order> showOrders()
        {
            List<Order> orders = new List<Order>();
            conn.Open();
            SqlCommand UpdateCmd = new SqlCommand("select [Order].id ,[Order].[date],[Order].total_price from[Order]where[user_id] = @email", conn);

            SqlParameter p1 = new SqlParameter("@email", loggedUser.Email);
            UpdateCmd.Parameters.Add(p1);

            SqlDataReader reader = UpdateCmd.ExecuteReader();
            while (reader.Read())
            {
                Order o = new Order();
                o.Date = reader.GetDateTime(reader.GetOrdinal("date"));
                o.Id = reader["id"].ToString();
                o.TotalPrice= Convert.ToInt32(reader["total_price"]);
                orders.Add(o);
            }
            reader.Close();
            conn.Close();
            return orders;
        }
    
    }
}

