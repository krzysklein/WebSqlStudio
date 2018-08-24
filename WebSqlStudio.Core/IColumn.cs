using System;
using System.Collections.Generic;
using System.Text;

namespace WebSqlStudio.Core
{
    public interface IColumn
    {
        string Name { get; }
        string SqlType { get; }
        bool IsNullable { get; }
    }
}
