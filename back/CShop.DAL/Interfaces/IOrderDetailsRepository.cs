using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CShop.Common.Entities;

namespace CShop.DAL.Interfaces
{
    public interface IOrderDetailsRepository
    {
        public Task<OrderDetails> GetByIdAsync(Guid id);
        public Task<IEnumerable<OrderDetails>> GetAllAsync();
        public Task InsertAsync(OrderDetails orderDetails);
        public Task UpdateAsync(OrderDetails orderDetails);
        public Task DeleteAsync(Guid id);
    }
}