using System;

namespace WebSqlStudio.Core
{
    public abstract class DatabaseConnectionBase : IDatabaseConnection
    {
        public string Name { get; private set; }
        public string ConnectionString { get; protected set; }
        public DatabaseConnectionState State { get; protected set; }
        public abstract IDatabaseSystem DatabaseSystem { get; }

        protected DatabaseConnectionBase(string name)
        {
            Name = name;
            State = DatabaseConnectionState.Disconnected;
        }

        public abstract void Connect(string connectionString);
        public abstract void Disconnect();
    }
}
