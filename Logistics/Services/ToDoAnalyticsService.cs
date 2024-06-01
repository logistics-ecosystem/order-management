using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using Logistics.Models;
using Logistics.DTO;
using Logistics.DBContext;
using Logistics.Protos;
using Microsoft.AspNetCore.SignalR;

namespace Logistics.Services
{
    public class ToDoAnalyticsService : ToDoAnalytics.ToDoAnalyticsBase
    {
        private readonly IPostgreService _context;

        public ToDoAnalyticsService(IPostgreService context)
        {
            _context = context;
        }



        public override async Task<GetOrdersByFiltersResponse> GetOrdersByFilters(GetOrdersByFiltersRequest request, ServerCallContext context)
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
                LoadingType = request.LoadingType,
                TrunkType = request.TrunkType,
                Weight = request.Weight,
                LoadingMetre = request.LoadingMetre,
                Height = request.Height,
                Temperature = request.Temperature,
                Price = request.Price,
                DateTimeFrom = DateTimeFrom,
                DateTimeTo = DateTimeTo,
                ContactInfo = request.ContactInfo,
                Distance = request.Distance
            };

            var orders = await _context.GetOrdersByFilters(orderQuery);
            var response = new GetOrdersByFiltersResponse();
            foreach (var order in orders)
            {
                response.Orders.Add(new OrderResponse
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


        public override async Task<GetAcceptedOrdersResponse> GetAcceptedOrders(GetAcceptedOrdersRequest request, ServerCallContext context)
        {

            DateTime DataTimeAccepted;
            try
            {
                DataTimeAccepted = DateTime.Parse(request.DateTimeAccepted);
            }
            catch (ArgumentNullException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Null DateTime"));
            }
            catch (FormatException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid DateTime format"));
            }
            if (!Guid.TryParse(request.OrderId, out var orderID) & !Guid.TryParse(request.CarId, out var carID))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid OrderId format"));
            }

            var orders = await _context.GetAcceptedOrders(null, DataTimeAccepted, orderID, carID);
            var response = new GetAcceptedOrdersResponse();
            foreach (var acceptedOrder in orders)
            {
                response.Orders.Add(new AcceptedOrderResponse
                {
                    DateTimeAccepted = acceptedOrder.DateTimeAccepted.ToString("o"),
                    OrderId = acceptedOrder.OrderId.ToString(),
                    CarId = acceptedOrder.CarId.ToString()
                });
            }
            return await Task.FromResult(response);
        }
    }
}
