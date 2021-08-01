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
    public class ProductRepository : IProductRepository
    {
        private readonly CShopDbContext _context;

        public ProductRepository(CShopDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ToListAsync();
        }

        public async Task InsertAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var local = _context.Products.Local.FirstOrDefault(p => p.Id == product.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new NullReferenceException($"Product with {product.Id} id doesn't exists");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}