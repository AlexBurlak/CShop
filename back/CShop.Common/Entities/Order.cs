using System;

namespace CShop.Common.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}