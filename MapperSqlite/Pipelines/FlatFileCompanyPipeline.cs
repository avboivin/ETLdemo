using MapperSqlite.Load.XmlLoadModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite.Pipelines
{
    class FlatFileCompanyPipeline: ETL<FlatfileExtractorManager,FlatfileToXmlTransformModule,XmlMaker>
    {
        public FlatFileCompanyPipeline(string ConfigFile) :base(ConfigFile)
        {

        }
    }
}
