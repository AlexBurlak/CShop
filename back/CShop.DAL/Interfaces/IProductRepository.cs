using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CShop.Common.Entities;

namespace CShop.DAL.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> GetByIdAsync(Guid id);
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task InsertAsync(Product product);
        public Task UpdateAsync(Product product);
        public Task DeleteAsync(Guid id);
    }
}