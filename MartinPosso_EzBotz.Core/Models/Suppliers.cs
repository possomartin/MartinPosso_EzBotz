using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Suppliers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Products> Products { get; set; }

        public static ObservableCollection<Suppliers> GetSuppliers(string connectionString)
        {
            string get = "select Id, Name, Address from Suppliers where Name is not null";

            var suppliers = new ObservableCollection<Suppliers>();

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
                                    var supplier = new Suppliers();
                                    supplier.Id = reader.GetInt32(0);
                                    supplier.Name = reader.GetString(1);
                                    supplier.Address = reader.GetString(2);

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
    }
}
