using System;
using System.Collections.Generic;

#nullable disable

namespace MapperSqlite.Transform.schema
{
    public partial class Representative
    {
        public Representative()
        {
            Clients = new HashSet<Client>();
        }

        public string Name { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Repnumber { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
