using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WebSqlStudio.Core;

namespace WebSqlStudio.Providers.SqlServer
{
    public class SqlServerDatabase : IDatabase
    {
        public SqlServerDatabaseSystem DatabaseSystem { get; private set; }

        public string Name { get; private set; }
        public IEnumerable<ITable> Tables => _FetchTables().ToList();

        public SqlServerDatabase(SqlServerDatabaseSystem databaseSystem, string name)
        {
            DatabaseSystem = databaseSystem;
            Name = name;
        }

        private IEnumerable<ITable> _FetchTables()
        {
            var query = $@"SELECT TABLE_NAME, TABLE_SCHEMA, TABLE_TYPE FROM [{Name}].[INFORMATION_SCHEMA].[TABLES]";
            var result = DatabaseSystem.ExecuteQuery(query);
            foreach (DataRow row in result.DataSet.Tables[0].Rows)
            {
                TableType tableType;

                switch ((string)row[2])
                {
                    case "BASE TABLE":
                        tableType = TableType.Table;
                        break;

                    case "VIEW":
                        tableType = TableType.View;
                        break;

                    default:
                        continue;
                }

                yield return new SqlServerTable(this, (string)row[0], (string)row[1], tableType);
            }
        }
    }
}
