using System;
using System.Collections.Generic;
using System.Text;

namespace WebSqlStudio.Core
{
    public enum DatabaseConnectionState : int
    {
        Disconnected = 0,
        Connecting = 1,
        Connected = 2
    }
}
