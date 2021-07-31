using System;

namespace CShop.Common.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Size { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitWeight { get; set; }
        public bool ProductAvailable { get; set; }
        public bool DiscountAvailable { get; set; }
        public string PicturePath { get; set; }
        public decimal Ranking { get; set; }
    }
}