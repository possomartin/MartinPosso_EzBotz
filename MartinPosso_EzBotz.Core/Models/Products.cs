using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Products
    {
        public int Id { get; set; }
        
        public int CategoryID { get; set; }
        public Categories Categories { get; set; }

        public int Stock { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SupplierID { get; set; }
        public Suppliers Suppliers { get; set; }

        public static void AddData(String connectionString, int CategoryID, int stock, string name, string description, int supplierID)
        {
            string add = "INSERT INTO Products (CategoryID, Stock, Name, Description, SupplierID) VALUES ('" + CategoryID + "','" + stock + "','" + name + "','" + description +"','" + supplierID + "')"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if(connection.State == System.Data.ConnectionState.Open)
                {
                    using(SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = add;
                        cmd.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

    }
}
