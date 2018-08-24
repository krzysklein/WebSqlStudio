using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSqlStudio.Core
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        public List<IDatabaseConnectionProvider> Registrations { get; private set; }
        public IEnumerable<string> Providers => Registrations.Select(t => t.Name);

        public DatabaseConnectionFactory()
        {
            Registrations = new List<IDatabaseConnectionProvider>();
        }

        public IDatabaseConnection CreateDatabaseConnection(string providerName, string connectionName)
        {
            var provider = Registrations.SingleOrDefault(t => t.Name == providerName);
            if (provider == null)
            {
                throw new ArgumentException($"Unknown provider {providerName}");
            }

            var connection = provider.CreateDatabaseConnection(connectionName);
            return connection;
        }

        public void RegisterProvider<T>() where T : IDatabaseConnectionProvider, new()
        {
            var provider = new T();

            if (Registrations.Any(t => t.Name == provider.Name))
            {
                throw new ArgumentException($"Provider {provider.Name} is already registered");
            }

            Registrations.Add(provider);
        }
    }
}
