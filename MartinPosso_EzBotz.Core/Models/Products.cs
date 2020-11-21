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
        
        public virtual Categories Categories { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SupplierID { get; set; }

        public virtual Suppliers Suppliers { get; set; }

        public static void AddData(String connectionString, int CategoryID, string name, string description, int supplierID)
        {
            string add;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

            }
        }

    }
}
