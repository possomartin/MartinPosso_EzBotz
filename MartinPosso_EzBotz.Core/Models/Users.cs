using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public static void AddData(string connectionString, string name, string email, string password)
        {
            string add = "INSERT INTO Users (Name,Email,Password) VALUES ('" + name + "','" + email + "','" + password + "')";

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

        public static bool Exists(string connectionString, string email, string password)
        {
            string get = "SELECT * FROM Users WHERE Email='" + email + "' and Password= '" + password + "'";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using(SqlDataAdapter sda = new SqlDataAdapter(get, connection))
                {
                    DataTable data = new DataTable();
                    sda.Fill(data);

                    if(data.Rows.Count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                connection.Close();
            }

        }
    }
}
