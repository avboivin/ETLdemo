using MapperSqlite.Interfaces;
using MapperSqlite.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite
{
    public static class ETLFactory
    {
        public static IETL GetETL(string PipelineName, string ConfigFilePath)
        {
            if (PipelineName == "flatfilecompany")
                return new FlatFileCompanyPipeline(ConfigFilePath);
            else
                throw new NotImplementedException();
        }
    }
}
