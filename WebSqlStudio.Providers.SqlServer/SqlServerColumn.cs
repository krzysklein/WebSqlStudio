using System;
using System.Collections.Generic;
using System.Text;
using WebSqlStudio.Core;

namespace WebSqlStudio.Providers.SqlServer
{
    public class SqlServerColumn : IColumn
    {
        public string Name { get; private set; }
        public string SqlType { get; private set; }
        public bool IsNullable { get; private set; }

        public SqlServerColumn(string name, string sqlType, bool isNullable)
        {
            Name = name;
            SqlType = sqlType;
            IsNullable = IsNullable;
        }
    }
}
