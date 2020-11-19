using System;
using System.Collections.Generic;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int SupplierID { get; set; }

    }
}
