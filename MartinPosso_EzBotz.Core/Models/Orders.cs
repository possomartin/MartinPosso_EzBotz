using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{

    public class Orders
    {
        public int Id { get; set; }
        public int PeopleID {get; set;}
        public decimal Total { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }

        public static ObservableCollection<Orders> GetOrders(string connectionString)
        {
            string get = "select Id, PeopleID, Total, UserID, ProductID, Price from Orders where Id is not null";

            var orders = new ObservableCollection<Orders>();

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
                                    var order = new Orders();

                                    order.Id = reader.GetInt32(0);
                                    order.PeopleID = reader.GetInt32(1);
                                    order.Total = reader.GetDecimal(2);
                                    order.UserID = reader.GetInt32(3);
                                    order.ProductID = reader.GetInt32(4);
                                    order.Price = reader.GetDecimal(5);
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

        public static void AddData(string connectionString, int ClientID, decimal total, int UserID, int ProductID, decimal Price)
        {
            string add = "INSERT INTO Orders (PeopleID, Total, UserID, ProductID, Price) VALUES (" + ClientID + "," + total + "," + UserID + "," + ProductID + "," + Price + ")";

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
            string delete = "delete from Orders where Id=" + id;

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

        public static void UpdateData(String connectionString, int PeopleID, decimal total, int userID, int productID, decimal price, int orderID)
        {
            string update = "update Orders set PeopleID =" + PeopleID + ", Total =" + total + ", UserID = '" + userID + "', ProductID ='" + productID + "', Price =" + price + " where Id = " + orderID;

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
