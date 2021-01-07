using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static void AddData(string connectionString, string name, string email, string password)
        {
            string add = "INSERT INTO Users (UserName,Email,Password) VALUES ('" + name + "','" + email + "','" + password + "')";

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

        public static ObservableCollection<User> GetUsers(string connectionString)
        {
            string get = "select UserID, UserName, Email, Password from Users where UserName is not null";

            var users = new ObservableCollection<User>();

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
                                    var user = new User();
                                    user.UserID = reader.GetInt32(0);
                                    user.UserName = reader.GetString(1);
                                    user.Email = reader.GetString(2);
                                    user.Password = reader.GetString(3);
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

        public static void Delete(string connectionString, int id)
        {
            string delete = "delete from Users where UserID=" + id;

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

        public static void UpdateData(String connectionString, string name, string email, string password, int userID)
        {
            string update = "update Users set UserName ='" + name + "', Email = '" + email + "', Password ='" + password + "' where UserID = " + userID;

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
