using System;
using Microsoft.AspNetCore.Identity;

namespace BookstoreAPI.Models
{
	public class CartItem
	{
        public int Id { get; set; }
        public string? UserId { get; set; } // Foreign key to IdentityUser
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public IdentityUser? User { get; set; } // Navigation property to IdentityUser
    }
}

