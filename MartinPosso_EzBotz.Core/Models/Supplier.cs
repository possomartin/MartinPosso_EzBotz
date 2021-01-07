using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public static ObservableCollection<Supplier> GetSuppliers(string connectionString)
        {
            string get = "select SupplierID, SupplierName, Address, Email from Suppliers where SupplierName is not null";

            var suppliers = new ObservableCollection<Supplier>();

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

                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while(reader.Read())
                                {
                                    var supplier = new Supplier();
                                    supplier.SupplierID = reader.GetInt32(0);
                                    supplier.SupplierName = reader.GetString(1);
                                    supplier.Address = reader.GetString(2);
                                    supplier.Email = reader.GetString(3);

                                    suppliers.Add(supplier);
                                }
                            }
                        }
                    }
                }

                return suppliers;
            }
            catch(Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public static void AddData(string connectionString, string name, string address, string email)
        {
            string add = "INSERT INTO Suppliers (SupplierName, Address, Email) values ('" + name + "','" + address + "','" + email +  "')";

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

        public static void UpdateData(String connectionString, string name, string address, string email, int supplierID)
        {
            string update = "update Suppliers set SupplierName ='" + name + "', Address ='" + address + "', Email ='" + email + "' where SupplierID = " + supplierID;

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
            string delete = "delete from Suppliers where SupplierID=" + id;

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
