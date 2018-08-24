namespace WebSqlStudio.Core
{
    public interface IDatabaseConnection
    {
        string Name { get; }
        string ConnectionString { get; }
        DatabaseConnectionState State { get; }
        IDatabaseSystem DatabaseSystem { get; }

        void Connect(string connectionString);
        void Disconnect();
    }
}