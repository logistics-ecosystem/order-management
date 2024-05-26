using Logistics.Models;
using Logistics.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> AddNewOrder([FromBody] Available order)
        {
            await _orderService.AddNewAvailableOrderAsync(order);
            return Ok();
        }

        [HttpGet("check-deadlines")]
        public async Task<ActionResult<List<Deadline>>> CheckDeadlineOrders()
        {
            var deadlineOrders = await _orderService.CheckDeadlineOrdersAsync();
            return Ok(deadlineOrders);
        }

        [HttpGet]
        public async Task<ActionResult<List<Available>>> GetAllOrders([FromQuery] OrderQuery query)
        {
            var orders = await _orderService.GetAllOrdersAsync(query);
            return Ok(orders);
        }        

        [HttpGet("{id}")]
        public async Task<ActionResult<Available>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderById(Guid id)
        {
            await _orderService.DeleteOrderByIdAsync(id);
            return NoContent();
        }
    }
}
