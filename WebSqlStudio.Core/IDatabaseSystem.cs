using System;
using System.Collections.Generic;
using System.Text;

namespace WebSqlStudio.Core
{
    public interface IDatabaseSystem
    {
        IEnumerable<IDatabase> Databases { get; }
        IDatabase CurrentDatabase { get; }

        void ChangeCurrentDatabase(IDatabase database);
        QueryResult ExecuteQuery(string query);
    }
}
