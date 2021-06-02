using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite.Transform
{
    /// <summary>
    /// This output represents a representatives with all his client and their orders plus some product details.
    /// </summary>
    public class RepOutput
    {
        public string RepName { get; set; }
        public string RepId { get; set; }
        public List<RepClients> clients { get; set; }

    }

    public class RepClients
    {
        public int ClientId { get; set; }
        public int TotalOrders { get; set; }
        public List<ClientOrders> orders { get; set; }

    }

    public class ClientOrders
    {
        public int OrderId { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderTime { get; set; }
        public List<string> ProductNames { get; set; }
    }
}
