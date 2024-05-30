using Logistics.DTO;
using Logistics.Models;

namespace Logistics.Services
{
    public interface IPostgreService
    {
        public Task<Order?> GetOrderById(Guid id);
        public Task AddNewOrder(OrderDTO orderDto);
        public Task<List<Order>> GetOrdersByFilters(OrderQuery orderQuery);
        public Task<List<AcceptedOrder>> GetAcceptedOrders(
            Guid? uniqueId,
            DateTime? dateTimeAccepted,
            int? orderId,
            Guid? carId
            );
    }
}
