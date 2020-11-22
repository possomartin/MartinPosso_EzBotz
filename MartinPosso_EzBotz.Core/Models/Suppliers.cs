using System;
using System.Collections.Generic;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{
    public class Suppliers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Products> Products { get; set; }
    }
}
