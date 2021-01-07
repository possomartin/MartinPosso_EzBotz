using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{

    public class Category
    {

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string description { get; set; }

        public List<Product> ListaProductos;
        public Category()
        {
            ListaProductos = new List<Product>();
        }

        public static ObservableCollection<Category> GetCategories(string connectionString)
        {
            string get = "select CategoryID, CategoryName, Description from categories where CategoryName is not null";

            var categories = new ObservableCollection<Category>();

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if(connection.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = get;

                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while(reader.Read())
                                {
                                    var category = new Category();
                                    category.CategoryID = reader.GetInt32(0);
                                    category.CategoryName = reader.GetString(1);
                                    category.description = reader.GetString(2);
                                    categories.Add(category);
                                }
                            }
                        }
                    }
                }
                return categories;
            }
            catch(Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public static void AddData(string connectionString, string name, string description)
        {
            string add = "INSERT INTO Categories (CategoryName, Description) VALUES ('" + name + "','" + description + "')";

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

        public static void UpdateData(String connectionString, string name, string description, int categoryID)
        {
            string update = "update Categories set CategoryName ='" + name + "', description ='" + description + "' where CategoryID = " + categoryID;

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

        public static void Delete(string connectionString, int id)
        {
            string delete = "delete from Categories where CategoryID=" + id;

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

        public override string ToString()
        {
            return "Category: " + CategoryName;
        }
    }
}
