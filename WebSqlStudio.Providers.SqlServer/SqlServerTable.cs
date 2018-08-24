using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WebSqlStudio.Core;

namespace WebSqlStudio.Providers.SqlServer
{
    public class SqlServerTable : ITable
    {
        public SqlServerDatabase Database { get; private set; }
        public string Name { get; private set; }
        public string Schema { get; private set; }
        public TableType Type { get; private set; }
        public IEnumerable<IColumn> Columns => _FetchColumns().ToList();

        public SqlServerTable(SqlServerDatabase database, string name, string schema, TableType type)
        {
            Database = database;
            Name = name;
            Schema = schema;
            Type = type;
        }

        private IEnumerable<IColumn> _FetchColumns()
        {
            var query = $@"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM [{Database.Name}].[INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_SCHEMA] = '{Schema}' AND [TABLE_NAME] = '{Name}'";
            var result = Database.DatabaseSystem.ExecuteQuery(query);
            foreach (DataRow row in result.DataSet.Tables[0].Rows)
            {
                bool isNullable = (string)row[2] == "YES";
                yield return new SqlServerColumn((string)row[0], (string)row[1], isNullable);
            }
        }
    }
}
