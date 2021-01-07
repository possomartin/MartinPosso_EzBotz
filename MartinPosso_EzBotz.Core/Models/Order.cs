using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{

    public class Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public static ObservableCollection<Order> GetOrders(string connectionString)
        {
            string get = "select OrderID, ProductID, PersonID, Price, Quantity from Orders where OrderID is not null";

            var orders = new ObservableCollection<Order>();

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    if(connection.State == System.Data.ConnectionState.Open)
                    {
                        using(SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = get;

                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while(reader.Read())
                                {
                                    var order = new Order();

                                    order.OrderID = reader.GetInt32(0);
                                    order.ProductID = reader.GetInt32(1);
                                    order.PersonID = reader.GetInt32(2);
                                    order.Price = reader.GetDouble(3);
                                    order.Quantity = reader.GetInt32(4);

                                    orders.Add(order);
                                }
                            }
                        }
                    }
                    return orders;
                }
            }
            catch(Exception eSql)
            {
                Debug.WriteLine("Error: " + eSql);
            }
            return null;
        }

        public static void AddData(string connectionString, int ProductID, int PersonID, double Price, int Quantity)
        {
            string add = "INSERT INTO Orders (ProductID, PersonID, Price, Quantity) VALUES (" + ProductID + "," + PersonID + "," + Price + "," + Quantity + ")";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = add;
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        public static void Delete(string connectionString, int id)
        {
            string delete = "delete from Orders where OrderID=" + id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = delete;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void UpdateData(String connectionString, int ProductID, int PersonID, double Price, int Quantity, int orderID)
        {
            string update = "update Orders set ProductID =" + ProductID + ", PersonID =" + PersonID + ", Price = " + Price + ", Quantity =" + Quantity + " where OrderID = " + orderID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = update;
                        cmd.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }

        }
    }

}
