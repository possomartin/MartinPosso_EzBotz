using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int ProductCode { get; set; }
        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        public static void AddData(String connectionString, string productName, string description, int stock, int productCode, int supplierID, int categoryID, string image, double price)
        {
            string add = "INSERT INTO Products (ProductName, Description, Stock, ProductCode, SupplierID, CategoryID, Image, Price) VALUES ('" + productName + "','" + description + "'," + stock + "," + productCode + "," + supplierID + "," + categoryID + ",'" + image + "'," + price + ")";

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

        public static ObservableCollection<Product> GetProducts(string connectionString)
        {
            string get = "select ProductID, ProductName, Description, Stock, ProductCode, SupplierID, CategoryID, Image, Price from Products where ProductID is not null";

            var products = new ObservableCollection<Product>();

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
                                    var product = new Product();

                                    product.ProductID = reader.GetInt32(0);
                                    product.ProductName = reader.GetString(1);
                                    product.Description = reader.GetString(2);
                                    product.Stock = reader.GetInt32(3);
                                    product.ProductCode = reader.GetInt32(4);
                                    product.SupplierID = reader.GetInt32(5);
                                    product.CategoryID = reader.GetInt32(6);
                                    product.Image = reader.GetString(7);
                                    product.Price = reader.GetDouble(8);

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

        public static ObservableCollection<Product> OrderBy(string connectionString, string order)
        {
            string get = "select ProductID, ProductName, Description, Stock, ProductCode, SupplierID, CategoryID, Image, Price from Products where ProductName is not null ORDER BY ProductID " + order;

            var products = new ObservableCollection<Product>();

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
                                    var product = new Product();

                                    product.ProductID = reader.GetInt32(0);
                                    product.ProductName = reader.GetString(1);
                                    product.Description = reader.GetString(2);
                                    product.Stock = reader.GetInt32(3);
                                    product.ProductCode = reader.GetInt32(4);
                                    product.SupplierID = reader.GetInt32(5);
                                    product.CategoryID = reader.GetInt32(6);
                                    product.Image = reader.GetString(7);
                                    product.Price = reader.GetDouble(8);
                                
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
            string delete = "delete from Products where ProductID=" + id;

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

        public static void UpdateData(String connectionString, string productName, string description, int stock, int productCode, int supplierID, int categoryID, string image, double price, int productID)
        {
            string update = "update Products set ProductName= '" + productName + "', Description= '" + description + "', Stock= " + stock + ", ProductCode= " + productCode + ", SupplierID= " + supplierID + ", CategoryID= " + categoryID + ", Image= '" + image + "', Price=" + price + " where ProductID = " + productID;

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
