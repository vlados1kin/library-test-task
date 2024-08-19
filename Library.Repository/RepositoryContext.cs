using Library.Domain.Models;
using Library.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors = null!;
    public DbSet<Book> Books = null!;
    public DbSet<Genre> Genres = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}