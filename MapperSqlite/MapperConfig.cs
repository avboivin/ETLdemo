using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MapperSqlite.Config
{
    public class MapperConfig
    {
        //TODO
        public configuration configs { get; set; }
        public Dictionary<string,string> definitions { get; set; }
        public List<table> tables { get; set; }
    }
    public class configuration
    {
        public string databasename { get; set; }
        public string description { get; set; } // what's the point ?
        public string filetype { get; set; }
        public string workdirectory { get; set; }
        public string delimitertypes { get; set; }
    }


    public class table
    {
        [YamlMember(Alias = "tablename", ApplyNamingConventions = false)]
        public string tablename { get; set; }
        public string filename { get; set; }
        public bool skip_header { get; set; }
        public List<column> columns { get; set; }
    }

    public class column
    {
        [YamlMember(Alias = "name", ApplyNamingConventions = false)]
        public string name { get; set; }
        public string type { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public string references { get; set; }
    }
}
