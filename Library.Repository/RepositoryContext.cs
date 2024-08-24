using Library.Domain.Models;
using Library.Repository.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new IssueConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}