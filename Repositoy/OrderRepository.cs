﻿using Microsoft.EntityFrameworkCore;
using Yum.Data;
using Yum.Repositoy.IRepository;

namespace Yum.Repositoy
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await _db.OrderHeader.AddAsync(orderHeader);
            await _db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _db.OrderHeader.Where(u => u.UserId == userId).ToListAsync();
            }
            return await _db.OrderHeader.ToListAsync();
        }

        public async Task<OrderHeader> GetAsync(int id)
        {
            return await _db.OrderHeader.Include(u => u.OrderDetails).FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<OrderHeader> GetOrderBySessionIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status, string paymentIntentId)
        {
            var orderHeader = _db.OrderHeader.FirstOrDefault(u => u.Id == orderId);
            if (orderHeader != null)
            {
                orderHeader.Status = status;
                await _db.SaveChangesAsync();
            }
            return orderHeader;
        }
    }
}