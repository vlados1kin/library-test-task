using Library.Domain.Models;
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
}