using System;
using System.Collections.Generic;
using System.Text;

namespace WebSqlStudio.Core
{
    public interface IDatabaseConnectionProvider
    {
        string Name { get; }

        IDatabaseConnection CreateDatabaseConnection(string connectionName);
    }
}
