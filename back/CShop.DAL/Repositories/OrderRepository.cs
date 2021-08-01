using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CShop.Common.Entities;
using CShop.DAL.Context;
using CShop.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CShop.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CShopDbContext _context;

        public OrderRepository(CShopDbContext context)
        {
            _context = context;
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.Customer).ToListAsync();
        }

        public async Task InsertAsync(Order order)
        {
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            var local = _context.Orders.Local.FirstOrDefault(o => o.Id == order.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NullReferenceException($"The order with {id} id doesn't exists");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}