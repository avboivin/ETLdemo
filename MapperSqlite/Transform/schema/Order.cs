using System;
using System.Collections.Generic;

#nullable disable

namespace MapperSqlite.Transform.schema
{
    public partial class Order
    {
        public long Clientid { get; set; }
        public long Orderid { get; set; }
        public double? Total { get; set; }
        public string Timestamp { get; set; }

        public virtual Client Client { get; set; }
    }
}
