using BookstoreAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BookstoreAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configure the relationship between CartItem and IdentityUser
        //    modelBuilder.Entity<CartItem>()
        //        .HasOne(c => c.User)
        //        .WithMany()
        //        .HasForeignKey(c => c.UserId);
        //}
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartView> cartViews { get; set; }
    }
}



