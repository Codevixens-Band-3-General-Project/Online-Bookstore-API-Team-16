using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string Genre { get; set; }
        public int? YearOfPublication { get; set; }
        public string Publisher { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Precision(18, 2)]
        public decimal SubTotal { get; set; }
    }

    public class CartView
    {
        [Key]
        public int ViewId { get; set; }
        public List<Cart> Items { get; set; }
        [Precision(18, 2)]
        public decimal Total { get; set; }
    }

}

