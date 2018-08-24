using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebSqlStudio.Core;

namespace WebSqlStudio.Providers.SqlServer
{
    public class SqlServerDatabaseSystem : IDatabaseSystem
    {
        public SqlServerDatabaseConnection Connection { get; private set; }
        public IEnumerable<IDatabase> Databases => _FetchDatabases().ToList();

        public IDatabase CurrentDatabase { get; private set; }

        public SqlServerDatabaseSystem(SqlServerDatabaseConnection sqlServerDatabaseConnection)
        {
            Connection = sqlServerDatabaseConnection;
        }

        public void ChangeCurrentDatabase(IDatabase database)
        {
            using (var command = new SqlCommand($"USE [{database.Name}]", Connection.SqlConnection))
            {
                command.ExecuteNonQuery();
                CurrentDatabase = database;
            }
        }

        public QueryResult ExecuteQuery(string query)
        {
            var dataSet = new DataSet();
            var messagesStringBuilder = new StringBuilder();

            try
            {
                using (var command = new SqlCommand(query, Connection.SqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        do
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);

                            dataSet.Tables.Add(dataTable);
                        }
                        while (!reader.IsClosed);
                    }
                }

                messagesStringBuilder.AppendLine("Success");
            }
            catch (Exception ex)
            {
                messagesStringBuilder.AppendLine($"Error: {ex.Message}");
            }

            var result = new QueryResult(dataSet, messagesStringBuilder.ToString());
            return result;
        }

        private IEnumerable<IDatabase> _FetchDatabases()
        {
            var query = @"SELECT Name FROM [master].[sys].[databases]";
            var result = ExecuteQuery(query);
            foreach (DataRow row in result.DataSet.Tables[0].Rows)
            {
                yield return new SqlServerDatabase(this, (string)row[0]);
            }
        }
    }
}
