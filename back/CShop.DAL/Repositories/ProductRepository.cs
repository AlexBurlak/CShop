using System;
using System.Collections.Generic;
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
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsQueryable().ToListAsync();
        }

        public async Task InsertAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var productToModify = await _context.Products.FindAsync(product.Id);
            if (productToModify == null)
            {
                throw new NullReferenceException($"Product with {product.Id} id doesn't exists");
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NullReferenceException($"Product with {product.Id} id doesn't exists");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}