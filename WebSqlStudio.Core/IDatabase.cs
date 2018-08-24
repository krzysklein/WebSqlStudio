using System;
using System.Collections.Generic;
using System.Text;

namespace WebSqlStudio.Core
{
    public interface IDatabase
    {
        string Name { get; }
        IEnumerable<ITable> Tables { get; }
    }
}
