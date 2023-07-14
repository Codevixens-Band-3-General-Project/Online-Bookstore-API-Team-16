using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Models
{
	public class CartItem
	{
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to IdentityUser
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public Book Book { get; set; }
        [Precision(18, 2)]
        public decimal SubTotal { get; set; }
    }
}

