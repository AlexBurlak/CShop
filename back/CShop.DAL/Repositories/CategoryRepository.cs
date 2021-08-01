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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CShopDbContext _context;

        public CategoryRepository(CShopDbContext context)
        {
            _context = context;
        }
        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task InsertAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var local = _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new NullReferenceException($"The category with {id} id doesn't exists");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}