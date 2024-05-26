namespace Logistics.DBContext
{
    public interface IMongoDBSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
