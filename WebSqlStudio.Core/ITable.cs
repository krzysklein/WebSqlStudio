using System;
using System.Collections.Generic;
using System.Text;

namespace WebSqlStudio.Core
{
    public interface ITable
    {
        string Name { get; }
        string Schema { get; }
        TableType Type { get; }
        IEnumerable<IColumn> Columns { get; }
    }
}
