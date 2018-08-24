using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WebSqlStudio.Core;

namespace WebSqlStudio.Providers.SqlServer
{
    public class SqlServerDatabaseConnection : DatabaseConnectionBase, IDatabaseConnection
    {
        public SqlConnection SqlConnection { get; private set; }
        public SqlServerDatabaseSystem SqlServerDatabaseSystem { get; private set; }
        public override IDatabaseSystem DatabaseSystem => SqlServerDatabaseSystem;

        public SqlServerDatabaseConnection(string name)
            : base(name)
        {
        }

        public override void Connect(string connectionString)
        {
            if (State != DatabaseConnectionState.Disconnected)
            {
                throw new InvalidOperationException($"Cannot connect when in state {State}");
            }

            try
            {
                State = DatabaseConnectionState.Connecting;

                SqlConnection = new SqlConnection(connectionString);
                SqlConnection.Open();

                ConnectionString = connectionString;
                State = DatabaseConnectionState.Connected;

                SqlServerDatabaseSystem = new SqlServerDatabaseSystem(this);
            }
            catch /*(Exception ex)*/
            {
                State = DatabaseConnectionState.Disconnected;
                throw; // TODO: Throw library-specific exception and set the InnerException to ex
            }
        }

        public override void Disconnect()
        {
            if (State == DatabaseConnectionState.Disconnected)
            {
                throw new InvalidOperationException($"Cannot disconnect when in state {State}");
            }

            SqlConnection.Close();
            State = DatabaseConnectionState.Disconnected;

            SqlServerDatabaseSystem = null;
        }
    }
}
