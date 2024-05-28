namespace Logistics.DBContext
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
