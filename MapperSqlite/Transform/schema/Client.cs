using System;
using System.Collections.Generic;

#nullable disable

namespace MapperSqlite.Transform.schema
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public long Clientid { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Telephone { get; set; }
        public string Birthdate { get; set; }
        public string Repid { get; set; }

        public virtual Representative Rep { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
