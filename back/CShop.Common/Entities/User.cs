using System;
using Microsoft.AspNetCore.Identity;

namespace CShop.Common.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? ImagePath { get; set; }
        
    }
}