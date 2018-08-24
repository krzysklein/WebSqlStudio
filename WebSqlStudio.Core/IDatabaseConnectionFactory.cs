using System.Collections.Generic;

namespace WebSqlStudio.Core
{
    public interface IDatabaseConnectionFactory
    {
        IEnumerable<string> Providers { get; }

        IDatabaseConnection CreateDatabaseConnection(string providerName, string connectionName);
        void RegisterProvider<T>() where T : IDatabaseConnectionProvider, new();
    }
}