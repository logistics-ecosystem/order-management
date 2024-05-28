using LogisticTask.Data;
using LogisticTask.DTO;
using Microsoft.AspNetCore.Mvc;
using LogisticTask.Models;
using LogisticTask.Services;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System;

namespace LogisticTask.Services
{
    public class PostgreService : IPostgreService
    {

        private readonly DataContext _context;
        public PostgreService(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task AddNewOrder(OrderDTO orderDto)
        {

            var newOrder = new Order
            {
                AddressFrom = orderDto.AddressFrom,
                AddressTo = orderDto.AddressTo,
                DateTimeFrom = orderDto.DateTimeFrom,
                DateTimeTo = orderDto.DateTimeTo,
                FrachtType = orderDto.FrachtType,
                Distance = orderDto.Distance,
                TrunkType = orderDto.TrunkType,
                Weight = orderDto.Weight,
                LoadingMetre = orderDto.LoadingMetre,
                Height = orderDto.Height,
                LoadingType = orderDto.LoadingType,
                Temperature = orderDto.Temperature,
                Price = orderDto.Price,
                ContactInfo = orderDto.ContactInfo
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AcceptedOrder>> GetAcceptedOrders(Guid? uniqueId, DateTime? dateTimeAccepted, int? orderId, Guid? carId)
        {
            var query = _context.AcceptedOrdersHistory.AsQueryable();

            if (uniqueId.HasValue)
            {
                query = query.Where(x => x.UniqueId == uniqueId.Value);
            }

            if (dateTimeAccepted.HasValue)
            {
                query = query.Where(x => x.DateTimeAccepted == dateTimeAccepted.Value);
            }

            if (orderId.HasValue)
            {
                query = query.Where(x => x.OrderId == orderId.Value);
            }

            if (carId.HasValue)
            {
                query = query.Where(x => x.CarId == carId.Value);
            }

            var acceptedOrders = await query.ToListAsync();
            return acceptedOrders;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.UniqueId == id);

            return order;
        }

        public async Task<List<Order>> GetOrdersByFilters(OrderQuery query)
        {
            var orders = _context.Orders.AsQueryable();

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
    
    }
}
