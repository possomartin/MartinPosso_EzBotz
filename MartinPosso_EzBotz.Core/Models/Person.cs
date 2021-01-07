using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public static ObservableCollection<Person> GetPeople (string connectionString)
        {
            string get = "select PersonID, PersonName, LastName, Address, PhoneNumber, UserID from People where PersonName is not null";

            var people = new ObservableCollection<Person>();

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
                                    var person = new Person();
                                    person.PersonID = reader.GetInt32(0);
                                    person.PersonName = reader.GetString(1);
                                    person.LastName = reader.GetString(2);
                                    person.Address = reader.GetString(3);
                                    person.PhoneNumber = reader.GetString(4);
                                    person.UserID = reader.GetInt32(5);

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

        public static void AddData(string connectionString, string Name, string LastName, string Address, string phoneNumber, int UserID)
        {
            string add = "INSERT INTO People (PersonName, LastName, Address, PhoneNumber, UserID) VALUES ('" + Name + "','" + LastName + "','" + Address + "','" + phoneNumber + "'," + UserID + ")";

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

        public static void UpdateData(String connectionString, string name, string lastname, string address, string phoneNumber,  int userID, int personID)
        {
            string update = "update People set PersonName ='" + name + "', LastName ='" + lastname + "', Address = '" + address + "', PhoneNumber = '" + phoneNumber + "', UserID = " + userID + " where PersonID = " + personID;

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
            string delete = "delete from People where PersonID=" + id;

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
