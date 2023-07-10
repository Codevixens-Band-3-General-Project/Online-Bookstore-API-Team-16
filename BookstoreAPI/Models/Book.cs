using System;
using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? BookTitle { get; set; }
        public string? BookAuthor { get; set; }
        public string? Genre { get; set; }
        public int? YearOfPublication { get; set; }
        public string? Publisher { get; set; }
    }

    public class CartItem
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
