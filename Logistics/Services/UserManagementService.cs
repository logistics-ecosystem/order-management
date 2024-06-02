using AutoMapper;
using Grpc.Core;
using Logistics.Models;
using Logistics.Protos;

namespace Logistics.Services
{
    public class UserManagementService(IPostgreService postgre, IOrderService order, IMapper mapper) : UserManagement.UserManagementBase
    {
        private readonly IPostgreService _postgre = postgre;
        private readonly IOrderService _order = order;
        private readonly IMapper _mapper = mapper;

        public override async Task<OrderInfoResponse> GetOrderInfo(OrderInfoRequest request, ServerCallContext context)
        {
            if(!Guid.TryParse(request.UniqueId, out var Id))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid OrderId argument"));
            }

            var orderAvailable = await _order.GetOrderByIdAsync(Id);
            if (orderAvailable is not null)
            {
                var order = _mapper.Map<Order>(orderAvailable);
                return await Task.FromResult(_mapper.Map<OrderInfoResponse>(order));
            }

            var orderInfo = await _postgre.GetOrderById(Id);

            if(orderInfo is not null)
            {
                return await Task.FromResult(_mapper.Map<OrderInfoResponse>(orderInfo));
            }
            
            throw new RpcException(new Status(StatusCode.NotFound, "No Order with such Id"));
            //return await Task.FromResult(new OrderInfoResponse
            //{
            //    AddressFrom = orderInfo.AddressFrom,
            //    AddressTo = orderInfo.AddressTo,
            //    DateTimeFrom = orderInfo.DateTimeFrom.ToString("o"),
            //    DateTimeTo = orderInfo.DateTimeTo.ToString("o"),
            //    FrachtType = orderInfo.FrachtType,
            //    Distance = orderInfo.Distance,
            //    TrunkType = orderInfo.TrunkType,
            //    Weight = orderInfo.Weight,
            //    LoadingMetre = orderInfo.LoadingMetre,
            //    Height = orderInfo.Height,
            //    LoadingType = orderInfo.LoadingType,
            //    Temperature = orderInfo.Temperature,
            //    Price = orderInfo.Price,
            //    ContactInfo = orderInfo.ContactInfo
            //});
        }

        public override async Task<AcceptOrderResponse> AcceptOrder(AcceptOrderRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.OrderId, out Guid orderId) & 
                !Guid.TryParse(request.CarId, out Guid carId))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid OrderId argument"));
            }

            var order = _order.GetOrderByIdAsync(orderId) ?? throw new RpcException(new Status(StatusCode.NotFound, "No Order with such Id"));
            
            await _order.DeleteOrderByIdAsync(orderId);
            await _postgre.AddNewOrder(_mapper.Map<Order>(order));
            await _postgre.AcceptOrder(orderId, carId);

            return await Task.FromResult(new AcceptOrderResponse());
        }

        public override async Task<SetOrderUnavailableResponse> SetOrderUnavailable(SetOrderUnavailableRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.UniqueId, out var Id))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid OrderId argument"));
            }

            var order = _order.GetOrderByIdAsync(Id) ?? throw new RpcException(new Status(StatusCode.NotFound, "No Order with such Id"));
            await _order.DeleteOrderByIdAsync(Id);            
            await _postgre.AddNewOrder(_mapper.Map<Order>(order));

            return await Task.FromResult(new SetOrderUnavailableResponse());
        }
    }
}
