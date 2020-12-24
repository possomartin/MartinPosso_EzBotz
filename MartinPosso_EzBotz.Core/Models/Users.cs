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
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static void AddData(string connectionString, string name, string lastname, string email, string password)
        {
            string add = "INSERT INTO Users (Name,LastName,Email,Password) VALUES ('" + name + "','" + lastname + "','" + email + "','" + password + "')";

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

        public static ObservableCollection<Users> GetUsers(string connectionString)
        {
            string get = "select Id, Name, LastName, Email, Password from Users where Name is not null";

            var users = new ObservableCollection<Users>();

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

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while(reader.Read())
                                {
                                    var user = new Users();
                                    user.Id = reader.GetInt32(0);
                                    user.Name = reader.GetString(1);
                                    user.LastName = reader.GetString(2);
                                    user.Email = reader.GetString(3);
                                    user.Password = reader.GetString(4);
                                    users.Add(user);
                                }
                            }
                        }
                    }
               }
                return users;
            }
            catch(Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
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
            }
        }

    }
}
