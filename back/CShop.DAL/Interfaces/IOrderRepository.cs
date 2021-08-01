using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CShop.Common.Entities;

namespace CShop.DAL.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> GetByIdAsync(Guid id);
        public Task<IEnumerable<Order>> GetAllAsync();
        public Task InsertAsync(Order order);
        public Task UpdateAsync(Order order);
        public Task DeleteAsync(Guid id);
    }
}