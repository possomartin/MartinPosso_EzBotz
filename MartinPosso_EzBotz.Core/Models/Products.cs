using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Products
    {
        public int Id { get; set; }

        public int CategoryID { get; set; }
        public virtual Categories Categories { get; set; }

        public int Stock { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }
        public int SupplierID { get; set; }
        public virtual Suppliers Suppliers { get; set; }

        public static void AddData(String connectionString, int CategoryID, int stock, string name, string description, int supplierID, string image, decimal price)
        {
            string add = "INSERT INTO Products (CategoryID, Stock, Name, Description, SupplierID, Image, Price) VALUES ('" + CategoryID + "','" + stock + "','" + name + "','" + description + "','" + supplierID + "','" + image + "'," + price + ")";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = add;
                        cmd.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

        public static ObservableCollection<Products> GetProducts(string connectionString)
        {
            string get = "select Id, CategoryID, Stock, Name, Description, SupplierID, Image, Price from Products where Name is not null";

            var products = new ObservableCollection<Products>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = get;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var product = new Products();

                                    product.Id = reader.GetInt32(0);
                                    product.CategoryID = reader.GetInt32(1);
                                    product.Stock = reader.GetInt32(2);
                                    product.Name = reader.GetString(3);
                                    product.Description = reader.GetString(4);
                                    product.SupplierID = reader.GetInt32(5);
                                    product.Image = reader.GetString(6);
                                    product.Price = reader.GetDecimal(7);

                                    products.Add(product);
                                }
                            }
                        }
                    }
                }
                return products;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;

        }

        public static ObservableCollection<Products> OrderBy(string connectionString, string order)
        {
            string get = "select Id, CategoryID, Stock, Name, Description, SupplierID, Image, Price from Products where Name is not null ORDER BY Id " + order + ", CategoryID " + order + ", Stock " + order + ", Name " + order + ", Description " + order + ", SupplierID " + order + ", Image " + order + ", Price " + order + "";

            var products = new ObservableCollection<Products>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = get;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var product = new Products();

                                    product.Id = reader.GetInt32(0);
                                    product.CategoryID = reader.GetInt32(1);
                                    product.Stock = reader.GetInt32(2);
                                    product.Name = reader.GetString(3);
                                    product.Description = reader.GetString(4);
                                    product.SupplierID = reader.GetInt32(5);
                                    product.Image = reader.GetString(6);
                                    product.Price = reader.GetDecimal(7);

                                    products.Add(product);
                                }
                            }
                        }
                    }
                }
                return products;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;

        }

        public static void Delete(string connectionString, int id)
        {
            string delete = "delete from Products where Id=" + id;

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

        public static void UpdateData(String connectionString, int CategoryID, int stock, string name, string description, int supplierID, int productID, string image, decimal price)
        {
            string update = "update Products set CategoryID =" + CategoryID + ", Stock =" + stock + ", Name = '" + name + "', Description ='" + description + "', SupplierID =" + supplierID + ", Image ='" + image + "' , Price= " + price + " where Id = " + productID;

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
