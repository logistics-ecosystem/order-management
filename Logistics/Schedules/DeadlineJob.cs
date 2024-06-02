using AutoMapper;
using Logistics.Models;
using Logistics.Services;
using Quartz;

namespace Logistics.Schedules
{
    public class DeadlineJob : IJob
    {
        private readonly IOrderService _orderService;
        private readonly IPostgreService _postgreService;
        private readonly IMapper mapper;

        public DeadlineJob(IOrderService orderService, IPostgreService postgreService, IMapper mapper)
        {
            this._orderService = orderService;
            this._postgreService = postgreService;
            this.mapper = mapper;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var deadlines = await _orderService.CheckDeadlineOrdersAsync();
            foreach (var deadline in deadlines) 
            { 
                var order = await _orderService.GetOrderByIdAsync(deadline.Id);
                await _orderService.DeleteOrderByIdAsync(deadline.Id);
                await _postgreService.AddNewOrder(mapper.Map<Order>(order));
            }            
        }
    }
}
