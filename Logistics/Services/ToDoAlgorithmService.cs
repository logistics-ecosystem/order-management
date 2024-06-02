using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using Logistics.Models;
using Logistics.DTO;
using Logistics.DBContext;
using Logistics.Protos;
using Microsoft.AspNetCore.SignalR;

namespace Logistics.Services
{
    public class ToDoAlgorithmService : ToDoAlgorithm.ToDoAlgorithmBase
    {
        private readonly IOrderService _orderService;

        public ToDoAlgorithmService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public override async Task<GetAvailableOrdersResponse> GetAvailableOrders(GetAvailableOrdersRequest request, ServerCallContext context)
        {
            DateTime DateTimeFrom, DateTimeTo;
            try
            {
                DateTimeFrom = DateTime.Parse(request.DateTimeFrom);
                DateTimeTo = DateTime.Parse(request.DateTimeTo);
            }
            catch (ArgumentNullException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Null DateTime"));
            }
            catch (FormatException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid DateTime format"));
            }

            var orderQuery = new OrderQuery
            {
                AddressFrom = request.AddressFrom,
                AddressTo = request.AddressTo,
                FrachtType = request.FrachtType,
                Distance = request.Distance,
                LoadingType = request.LoadingType,
                TrunkType = request.TrunkType,
                Weight = request.Weight,
                LoadingMetre = request.LoadingMetre,
                Height = request.Height,
                Temperature = request.Temperature,
                Price = request.Price,
                DateTimeFrom = DateTimeFrom,
                DateTimeTo = DateTimeTo,
                ContactInfo = request.ContactInfo
            };

            var orders = await _orderService.GetAllOrdersAsync(orderQuery);

            var response = new GetAvailableOrdersResponse();
            foreach (var order in orders)
            {
                response.Orders.Add(new AvailableOrderResponse
                {
                    UniqueId = order.UniqueId.ToString(),
                    AddressFrom = order.AddressFrom,
                    AddressTo = order.AddressTo,
                    DateTimeFrom = order.DateTimeFrom.ToString("o"),
                    DateTimeTo = order.DateTimeTo.ToString("o"),
                    FrachtType = order.FrachtType,
                    Distance = order.Distance,
                    TrunkType = order.TrunkType,
                    Weight = order.Weight,
                    LoadingMetre = order.LoadingMetre,
                    Height = order.Height,
                    LoadingType = order.LoadingType,
                    Temperature = order.Temperature,
                    Price = (float)order.Price,
                    ContactInfo = order.ContactInfo
                });
            }
            return await Task.FromResult(response);
        }
    }
}
