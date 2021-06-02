using System;
using System.Collections.Generic;

#nullable disable

namespace MapperSqlite.Transform.schema
{
    public partial class Ordersproduct
    {
        public long Orderid { get; set; }
        public long Itemid { get; set; }
        public long? Qty { get; set; }
        public double? Pricepaid { get; set; }

        public virtual Product Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
