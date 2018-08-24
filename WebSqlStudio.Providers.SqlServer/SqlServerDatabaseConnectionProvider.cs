using System;
using System.Collections.Generic;
using System.Text;
using WebSqlStudio.Core;

namespace WebSqlStudio.Providers.SqlServer
{
    public class SqlServerDatabaseConnectionProvider : IDatabaseConnectionProvider
    {
        public string Name => "SQL Server";

        public IDatabaseConnection CreateDatabaseConnection(string connectionName)
        {
            return new SqlServerDatabaseConnection(connectionName);
        }
    }
}
