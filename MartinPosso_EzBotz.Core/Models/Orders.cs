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
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int ClientID { get; set; }

        public virtual Users User { get; set; }
        public virtual Products Product { get; set; }
        public virtual People People { get; set; }

        public static ObservableCollection<Orders> GetOrders(string connectionString)
        {
            var orders = new ObservableCollection<Orders>();

            string get = "Select * from Orders where Id is not null";

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = get;
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var order = new Orders();
                                    order.Id = reader.GetInt32(0);
                                    order.ClientID = reader.GetInt32(1);
                                    order.Total = reader.GetDecimal(2);
                                    order.UserID = reader.GetInt32(3);
                                    order.ProductID = reader.GetInt32(4);
                                    order.Price = reader.GetDecimal(5);

                                    orders.Add(order);
                                }
                            }
                        }
                    }
                }
                return orders;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public static void AddData(string connectionString, int ClientID, decimal total, int UserID, int ProductID, decimal Price)
        {
            string add = "INSERT INTO Orders (ClientID, Total, UserID, ProductID, Price) VALUES (" + ClientID + "," + total + "," + UserID + "," + ProductID + "," + Price + ")";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                if(conn.State == System.Data.ConnectionState.Open)
                {
                    using(SqlCommand cmd = conn.CreateCommand())
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
    }
}
