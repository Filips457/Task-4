using LibManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibManager.Data;

public class LibraryContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne()
            .HasForeignKey(a => a.AuthorId);
    }
}