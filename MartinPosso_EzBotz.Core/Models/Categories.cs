using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MartinPosso_EzBotz.Core.Models
{

    public class Categories
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
