using Logistics.Models;
using Logistics.DTO;

namespace Logistics.Services
{
    public interface IOrderService
    {
        Task AddNewAvailableOrderAsync(Available order);
        Task<List<Deadline>> CheckDeadlineOrdersAsync();
        Task<List<Available>> GetAllOrdersAsync(OrderQuery query);        
        Task<Available?> GetOrderByIdAsync(Guid id);
        Task DeleteOrderByIdAsync(Guid id);
    }
}
