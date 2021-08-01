using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CShop.Common.Entities;

namespace CShop.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> GetByIdAsync(Guid id);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task InsertAsync(Category category);
        public Task UpdateAsync(Category category);
        public Task DeleteAsync(Guid id);
    }
}