using Logistics.DBContext;
using Logistics.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace Logistics.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Available> _available;
        private readonly IMongoCollection<Deadline> _deadline;
        public OrderService(IOptions<MongoDBSettings> mongodbsettings)
        {
            var mongoClient = new MongoClient(
                mongodbsettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongodbsettings.Value.DatabaseName);

            _available = mongoDatabase.GetCollection<Available>("Available");
            _deadline = mongoDatabase.GetCollection<Deadline>("Deadline");
        }

        public async Task AddNewAvailableOrderAsync(Available order)
        {
            await _available.InsertOneAsync(order);
        }

        public async Task<List<Deadline>> CheckDeadlineOrdersAsync()
        {
            return await _deadline
                .Find(order => order.DateTimeTo <= DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task DeleteOrderByIdAsync(Guid id)
        {
            await _available.DeleteOneAsync(order => order.UniqueId == id);
        }

        public async Task<List<Available>> GetAllOrdersAsync(OrderQuery query)
        {            
            var orders = _available.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.AddressFrom)) 
                orders = orders.Where(o => o.AddressFrom.Contains(query.AddressFrom));
            
            if (!string.IsNullOrWhiteSpace(query.AddressTo))            
                orders = orders.Where(o => o.AddressTo.Contains(query.AddressTo));
            
            if (query.DateTimeFrom.HasValue)
                orders = orders.Where(o => o.DateTimeFrom >= query.DateTimeFrom.Value);

            if (query.DateTimeTo.HasValue)
                orders = orders.Where(o => o.DateTimeTo <= query.DateTimeTo.Value);

            if (!string.IsNullOrWhiteSpace(query.FrachtType))
                orders = orders.Where(o => o.FrachtType == query.FrachtType);

            if (query.Distance.HasValue)            
                orders = orders.Where(o => o.Distance == query.Distance.Value);
            
            if (!string.IsNullOrWhiteSpace(query.TrunkType))            
                orders = orders.Where(o => o.TrunkType == query.TrunkType);
            
            if (query.Weight.HasValue)
                orders = orders.Where(o => o.Weight == query.Weight.Value);
            
            if (query.LoadingMetre.HasValue)
                orders = orders.Where(o => o.LoadingMetre == query.LoadingMetre.Value);
            
            if (query.Height.HasValue)
                orders = orders.Where(o => o.Height == query.Height.Value);
            
            if (!string.IsNullOrWhiteSpace(query.LoadingType))
                orders = orders.Where(o => o.LoadingType == query.LoadingType);
            
            if (query.Temperature.HasValue)
                orders = orders.Where(o => o.Temperature == query.Temperature.Value);
            
            if (query.Price.HasValue)
                orders = orders.Where(o => o.Price == query.Price.Value);
            
            if (!string.IsNullOrWhiteSpace(query.ContactInfo))
                orders = orders.Where(o => o.ContactInfo.Contains(query.ContactInfo));
            
            return await orders.ToListAsync();
        }

        public Task<Available> GetOrderByIdAsync(Guid id)
        {
            return _available.Find(order => order.UniqueId == id).FirstOrDefaultAsync();
        }       
    }
}
