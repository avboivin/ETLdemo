using MapperSqlite.Transform;
using MapperSqlite.Transform.schema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite
{
    public class FlatfileToXmlTransformModule : ITransformModule
    {
        public FlatfileToXmlTransformModule()
        {
        }

        //**Commands to regenerate the DB context**
        //dotnet tool install --global dotnet-ef
        //dotnet ef dbcontext scaffold "DataSource=C:\code\Mapper\Mapper\bin\Debug\net5.0\MapperDb.sqlite" Microsoft.EntityFrameworkCore.Sqlite --output-dir Transform/schema --project=../MapperSqlite --force
        
        public List<RepOutput> TransformData()
        {
            List<RepOutput> repOutputs = new List<RepOutput>();

            using (MapperDbContext context = new MapperDbContext())
            {
                repOutputs = context.Representatives.Select(rep => new RepOutput()
                {
                    RepId = rep.Repnumber,
                    RepName = rep.Name,
                    clients = rep.Clients.Select(cli => new RepClients()
                    {
                        ClientId = (int)cli.Clientid,
                        TotalOrders = cli.Orders.Count(),
                        orders = cli.Orders.Select(ord => new ClientOrders()
                        {
                            OrderId = (int)ord.Orderid,
                            OrderTime = DateTime.Parse(ord.Timestamp),
                            OrderTotal = ord.Total.Value,
                        }).ToList()
                    }).ToList()
                }).ToList();


                repOutputs.ForEach(
                    rep => rep.clients.ForEach(
                        cli => cli.orders.ForEach(
                            ord => ord.ProductNames = context.Ordersproducts.Where(
                                op => op.Orderid == ord.OrderId).Select(item => item.Item.Name).ToList())
                        )
                    );
            }
            return repOutputs;
        }
        public void MakeXml(List<RepOutput> reps,string Path)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<RepOutput>));
            System.IO.FileStream file = System.IO.File.Create(Path);
            writer.Serialize(file, reps);
            file.Close();
        }
    }
}



