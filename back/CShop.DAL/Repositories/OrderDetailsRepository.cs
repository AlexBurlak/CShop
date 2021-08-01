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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly CShopDbContext _context;

        public OrderDetailsRepository(CShopDbContext context)
        {
            _context = context;
        }
        public async Task<OrderDetails> GetByIdAsync(Guid id)
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                .ThenInclude(p => p.Category)
                .Include(od => od.Product)
                .ThenInclude(p => p.Seller)
                .FirstOrDefaultAsync(od => od.Id == id);
        }

        public async Task<IEnumerable<OrderDetails>> GetAllAsync()
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                .ThenInclude(p => p.Category)
                .Include(od => od.Product)
                .ThenInclude(p => p.Seller)
                .ToListAsync();
        }

        public async Task InsertAsync(OrderDetails orderDetails)
        {
            await _context.OrderDetails.AddAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderDetails orderDetails)
        {
            var local = _context.OrderDetails.Local.FirstOrDefault(od => od.Id == orderDetails.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(orderDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(od => od.Id == id);
            if (orderDetail == null)
            {
                throw new NullReferenceException($"The orderDetail with {id} id doesn't exists");
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
        }
    }
}