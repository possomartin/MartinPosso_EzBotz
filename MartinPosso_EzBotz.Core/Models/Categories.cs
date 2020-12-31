using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{

    public class Categories
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Products> ListaProductos;
        public Categories()
        {
            ListaProductos = new List<Products>();
        }

        public static ObservableCollection<Categories> GetCategories(string connectionString)
        {
            string get = "select Id, Name, Description from categories where Name is not null";

            var categories = new ObservableCollection<Categories>();

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
                                    var category = new Categories();
                                    category.Id = reader.GetInt32(0);
                                    category.Name = reader.GetString(1);
                                    category.Description = reader.GetString(2);
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
            string add = "INSERT INTO Categories (Name, Description) VALUES ('" + name + "','" + description + "')";

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
            string update = "update Categories set Name ='" + name + "', Description ='" + description + "' where Id = " + categoryID;

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
            string delete = "delete from Categories where Id=" + id;

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
            return "Category: " + Name;
        }
    }
}
