using MapperSqlite.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite
{
    public interface ILoadModule
    {
        public void OutputData(List<RepOutput> reps, string Outputpath);
    }
}
