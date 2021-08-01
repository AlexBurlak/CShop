using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CShop.Common.Entities;
using CShop.DAL.Context;
using CShop.DAL.Interfaces;
using CShop.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CShop.Tests
{
    public class DalProductTests
    {
        private readonly IProductRepository _repository;

        private List<Product> _mockProducts = new()
        {
            new Product()
            {
                Id = new Guid("F9168C5E-CEB2-4FAA-B6BF-329BF39FA1E4"),
                Name = "Product1",
                Description = "Product1Description",
                Category = null,
                CategoryId = null,
                Discount = 2.3m,
                DiscountAvailable = false,
                Ranking = 5m,
                Seller = null,
                SellerId = Guid.Empty,
                UnitPrice = 22,
                UnitWeight = 1.02m,
                Size = 0.98m,
                PicturePath = null,
                ProductAvailable = true
            },
            new Product()
            {
                Id = new Guid("329BF39F-CEB2-4FAA-B6BF-F9168C5EA1E4"),
                Name = "Product2",
                Description = "Product2Description",
                Category = null,
                CategoryId = null,
                Discount = 2.1m,
                DiscountAvailable = false,
                Ranking = 4m,
                Seller = null,
                SellerId = Guid.Empty,
                UnitPrice = 21,
                UnitWeight = 1.01m,
                Size = 0.99m,
                PicturePath = null,
                ProductAvailable = true
            },
        };
        public DalProductTests()
        {
            var contextOptions = new DbContextOptionsBuilder<CShopDbContext>()
                .UseInMemoryDatabase(databaseName: $"dal_product_test_{Guid.NewGuid()}")
                .Options;
            var context = new CShopDbContext(contextOptions);
            context.Products.AddRange(_mockProducts);
            context.SaveChanges();
            _repository = new ProductRepository(context);
        }
        [Fact]
        public async Task GetByIdAsync_WhenProductExists_ThenGetProduct()
        {
            //Arrange
            Guid id = new Guid("F9168C5E-CEB2-4FAA-B6BF-329BF39FA1E4");
            //Act
            var product = await _repository.GetByIdAsync(id);
            //Assert
            Assert.Equal(_mockProducts[0], product);
        }
        [Fact]
        public async Task GetByIdAsync_WhenProductNotExists_ThenGetNull()
        {
            //Arrange
            Guid id = new Guid("F9168C5E-0000-0000-0000-329BF39FA1E4");
            //Act
            var product = await _repository.GetByIdAsync(id);
            //Assert
            Assert.Null(product);
        }
        [Fact]
        public async Task GetAllAsync_WhenListNotEmpty_ThenGetList()
        {
            //Arrange
            //Act
            var products = (await _repository.GetAllAsync()).ToList();
            //Assert
            Assert.Equal(2, products.Count);
            Assert.Equal(_mockProducts[0], products[0]);
            Assert.Equal(_mockProducts[1], products[1]);
        }
        [Fact]
        public async Task InsertAsync_WhenProductIsValid_ThenProductInserts()
        {
            //Arrange
            var product = new Product()
            {
                Name = "Product3",
                Description = "Product3Description",
                Category = null,
                CategoryId = null,
                Discount = 1m,
                DiscountAvailable = false,
                Ranking = 4m,
                Seller = null,
                SellerId = Guid.Empty,
                UnitPrice = 1,
                UnitWeight = 1m,
                Size = 1m,
                PicturePath = null,
                ProductAvailable = true
            };
            //Act
            await _repository.InsertAsync(product);
            var checkResult = (await  _repository.GetAllAsync()).FirstOrDefault( c => c.Name == "Product3");
            //Assert
            Assert.NotNull(checkResult);
            Assert.Equal(product, checkResult);
        }
        [Fact]
        public async Task UpdateAsync_WhenProductExists_ThenProductUpdates()
        {
            //Arrange
            var product = new Product()
            {
                Id = new Guid("F9168C5E-CEB2-4FAA-B6BF-329BF39FA1E4"),
                Name = "Product3",
                Description = "Product3Description",
                Category = null,
                CategoryId = null,
                Discount = 1m,
                DiscountAvailable = false,
                Ranking = 4m,
                Seller = null,
                SellerId = Guid.Empty,
                UnitPrice = 1,
                UnitWeight = 1m,
                Size = 1m,
                PicturePath = null,
                ProductAvailable = true
            };
            //Act
            var previousProduct = await _repository.GetByIdAsync(product.Id);
            await _repository.UpdateAsync(product);
            var checkResult = await _repository.GetByIdAsync(product.Id);
            //Assert
            Assert.NotEqual(product, previousProduct);
            Assert.Equal(product, checkResult);
        }
        [Fact]
        public async Task UpdateAsync_WhenProductDoesntExists_ThenThrownException()
        {
            //Arrange
            var product = new Product()
            {
                Id = new Guid("F9168C5E-0000-0000-0000-329BF39FA1E4"),
                Name = "Product3",
                Description = "Product3Description",
                Category = null,
                CategoryId = null,
                Discount = 1m,
                DiscountAvailable = false,
                Ranking = 4m,
                Seller = null,
                SellerId = Guid.Empty,
                UnitPrice = 1,
                UnitWeight = 1m,
                Size = 1m,
                PicturePath = null,
                ProductAvailable = true
            };
            //Act
            //Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _repository.UpdateAsync(product));
        }
        [Fact]
        public async Task DeleteAsync_WhenProductExists_ThenProductDeletes()
        {
            //Arrange
            var productId = new Guid("329BF39F-CEB2-4FAA-B6BF-F9168C5EA1E4");
            //Act
            await _repository.DeleteAsync(productId);
            var actualResult = await _repository.GetByIdAsync(productId);
            //Assert
            Assert.Null(actualResult);
        }
        [Fact]
        public async Task DeleteAsync_WhenProductDoesntExists_ThenThrownException()
        {
            //Arrange
            var productId = new Guid("F9168C5E-0000-0000-0000-329BF39FA1E4");
            //Act
            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _repository.DeleteAsync(productId));
        }
    }
}