using MapperSqlite.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite
{
    public interface ITransformModule
    {
        public List<RepOutput> TransformData();
    }
}
