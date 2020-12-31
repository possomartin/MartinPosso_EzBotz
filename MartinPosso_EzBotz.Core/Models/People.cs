using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class People
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public virtual Users Users { get; set; }
        public List<Orders> Orders { get; set; }

        public static ObservableCollection<People> GetPeople (string connectionString)
        {
            string get = "select Id, Name, LastName, Address, Email, Telefono, UserID from People where Name is not null";

            var people = new ObservableCollection<People>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    if(conn.State == System.Data.ConnectionState.Open)
                    {
                        using(SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = get;
                            
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while(reader.Read())
                                {
                                    var person = new People();
                                    person.Id = reader.GetInt32(0);
                                    person.Name = reader.GetString(1);
                                    person.LastName = reader.GetString(2);
                                    person.Address = reader.GetString(3);
                                    person.Email = reader.GetString(4);
                                    person.Telefono = reader.GetString(5);
                                    person.UserID = reader.GetInt32(6);

                                    people.Add(person);
                                }
                            }
                        }
                    }
                }

                return people;
            }
            catch(Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public static void AddData(string connectionString, string Name, string LastName, string Address, string Email, string Telefono, int UserID)
        {
            string add = "INSERT INTO People (Name, LastName, Address, Email, Telefono, UserID) VALUES ('" + Name + "','" + LastName + "','" + Address + "','" + Email + "','" + Telefono + "'," + UserID + ")";

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

        public static void UpdateData(String connectionString, string name, string lastname, string address, string email, string telefono, int userID, int personID)
        {
            string update = "update People set Name ='" + name + "', LastName ='" + lastname + "', Address = '" + address + "', Email = '" + email + "', Telefono = '" + telefono + "', UserID = " + userID +  " where Id = " + personID;

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
            string delete = "delete from People where Id=" + id;

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
