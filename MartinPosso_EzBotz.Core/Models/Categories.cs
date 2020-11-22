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
                                    category.Description = "";
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

        public override string ToString()
        {
            return "Category: " + Name;
        }
    }
}
