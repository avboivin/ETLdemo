using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite
{

    public enum ExtractModuleEnum
    {
        csv,
        flat,
        xml,
        json
    }

    public enum TransformModuleEnum
    {
        FlatToXml,
        WhateverToWhatever
    }

    public enum LoadModuleEnum
    {
        xml,
        database
    }
}
