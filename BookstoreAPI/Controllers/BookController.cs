using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using BookstoreAPI.Extensions;
using BookstoreAPI.Models;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly ApplicationDbContext _db;

        public BookController(ILogger<BookController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Book>> Get()
        {
            // Retrieve all books from the database
            var books = _db.Books.ToList();
            return Ok(books);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Book>> Search(string searchTerm, string filter)
        {
            // Perform a search based on the provided search term and filter
            var books = _db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(filter))
            {
                switch (filter.ToLower())
                {
                    case "title":
                        // Split the search term by spaces to get individual keywords
                        var titleKeywords = searchTerm.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        books = books.ToList().Where(b => titleKeywords.Any(k => b.BookTitle.ToLower().Contains(k))).AsQueryable();
                        break;
                    case "author":
                        // Split the search term by spaces to get individual keywords
                        var authorKeywords = searchTerm.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        books = books.ToList().Where(b => authorKeywords.Any(k => b.BookAuthor.ToLower().Contains(k))).AsQueryable();
                        break;
                    case "genre":
                        // Split the search term by spaces to get individual keywords
                        var genreKeywords = searchTerm.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        books = books.ToList().Where(b => genreKeywords.Any(k => b.Genre.ToLower().Contains(k))).AsQueryable();
                        break;
                    default:
                        return BadRequest("Invalid filter parameter.");
                }
            }

            var filteredBooks = books.ToList();

            return Ok(filteredBooks);
        }



        [HttpGet("get-by-id/{id:int}")]
        public ActionResult<Book> Get(int id)
        {
            // Retrieve a book by its ID
            var book = _db.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("get-by-genre/{genre}")]
        public ActionResult<IEnumerable<Book>> GetByGenre(string genre)
        {
            // Retrieve books by genre
            var books = _db.Books
                .Where(b => b.Genre.ToLower().Contains(genre.ToLower()))
                .ToList();

            if (books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }


        [HttpGet("get-by-author/{author}")]
        public ActionResult<IEnumerable<Book>> GetByAuthor(string author)
        {
            // Retrieve books by author
            var books = _db.Books
                .Where(b => b.BookAuthor.ToLower().Contains(author.ToLower()))
                .ToList();

            if (books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Book>> CreateBook([FromForm] Book book)
        {
            // Create a new book and save it to the database
            Book newBook = new Book
            {
                BookTitle = book.BookTitle,
                YearOfPublication = book.YearOfPublication,
                Publisher = book.Publisher
            };

            // Split the authors by comma and remove any leading or trailing spaces
            if (!string.IsNullOrEmpty(book.BookAuthor))
            {
                string[] authors = book.BookAuthor.Split(',').Select(a => a.Trim()).ToArray();
                newBook.BookAuthor = string.Join(", ", authors);
            }

            // Split the genres by comma and remove any leading or trailing spaces
            if (!string.IsNullOrEmpty(book.Genre))
            {
                string[] genres = book.Genre.Split(',').Select(g => g.Trim()).ToArray();
                newBook.Genre = string.Join(", ", genres);
            }

            await _db.Books.AddAsync(newBook);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }


        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] Book updatedBook)
        {
            // Update an existing book based on its ID
            var existingBook = await _db.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(updatedBook.BookTitle))
            {
                existingBook.BookTitle = updatedBook.BookTitle;
            }

            if (!string.IsNullOrEmpty(updatedBook.BookAuthor))
            {
                existingBook.BookAuthor = updatedBook.BookAuthor;
            }

            if (!string.IsNullOrEmpty(updatedBook.Genre))
            {
                existingBook.Genre = updatedBook.Genre;
            }

            if (updatedBook.YearOfPublication.HasValue)
            {
                existingBook.YearOfPublication = updatedBook.YearOfPublication;
            }

            if (!string.IsNullOrEmpty(updatedBook.Publisher))
            {
                existingBook.Publisher = updatedBook.Publisher;
            }

            await _db.SaveChangesAsync();

            return Ok(existingBook);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Delete a book based on its ID
            var book = await _db.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("add-to-cart/{id:int}")]
        public async Task<ActionResult> AddToCart(int id)
        {
            // Add a book to the shopping cart
            var book = await _db.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            List<Book> cart = HttpContext.Session.GetObjectFromJson<List<Book>>("Cart") ?? new List<Book>();

            cart.Add(book);

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Ok();
        }

        [HttpPost("delete-from-cart/{id:int}")]
        public ActionResult DeleteFromCart(int id)
        {
            // Remove a book from the shopping cart
            List<Book> cart = HttpContext.Session.GetObjectFromJson<List<Book>>("Cart");

            if (cart == null)
            {
                return NotFound();
            }

            var book = cart.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                cart.Remove(book);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return Ok();
        }

        [HttpGet("view-cart")]
        public ActionResult<IEnumerable<Book>> ViewCart()
        {
            // View the contents of the shopping cart
            List<Book> cart = HttpContext.Session.GetObjectFromJson<List<Book>>("Cart");

            return Ok(cart);
        }
    }
}

