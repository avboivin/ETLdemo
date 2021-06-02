using MapperSqlite.Transform;
using System.Collections.Generic;

namespace MapperSqlite.Load.XmlLoadModule
{
    public class XmlMaker : ILoadModule
    {
        public XmlMaker() { }
        public void OutputData(List<RepOutput> reps, string OutputPath)
        {
            System.Xml.Serialization.XmlSerializer writer =
    new System.Xml.Serialization.XmlSerializer(typeof(List<RepOutput>));
            System.IO.FileStream file = System.IO.File.Create(OutputPath);
            writer.Serialize(file, reps);
            file.Close();
        }
    }
}
