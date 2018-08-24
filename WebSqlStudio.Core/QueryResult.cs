using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WebSqlStudio.Core
{
    public class QueryResult
    {
        public DataSet DataSet { get; private set; }
        public string Messages { get; private set; }

        public QueryResult(DataSet dataSet, string messages)
        {
            DataSet = dataSet;
            Messages = messages;
        }
    }
}
