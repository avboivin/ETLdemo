using System;
using System.Collections.Generic;

#nullable disable

namespace MapperSqlite.Transform.schema
{
    public partial class Product
    {
        public long Productid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
    }
}
