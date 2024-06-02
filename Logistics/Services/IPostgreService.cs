using Logistics.DTO;
using Logistics.Models;

namespace Logistics.Services
{
    public interface IPostgreService
    {
        public Task<Order?> GetOrderById(Guid id);
        public Task AddNewOrder(Order order);
        public Task<List<Order>> GetOrdersByFilters(OrderQuery orderQuery);
        public Task AcceptOrder(Guid orderId, Guid carId);
        public Task<List<AcceptedOrder>> GetAcceptedOrders(
            Guid? uniqueId,
            DateTime? dateTimeAccepted,
            Guid? orderId,
            Guid? carId
            );
    }
}
