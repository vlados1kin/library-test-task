using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Tests;

public class AuthorRepositoryTests
{
    private readonly AuthorRepository _authorRepository;
    private readonly RepositoryContext _context;

    public AuthorRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "Test").Options;

        _context = new RepositoryContext(options);
        _authorRepository = new AuthorRepository(_context);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAuthorByIdAsync_ShouldReturnAuthor_WhenAuthorExists()
    {
        var authorId = Guid.NewGuid();
        var author = new Author 
        { 
            Id = authorId, 
            FirstName = "Test", 
            LastName = "Author", 
            Country = "Country" 
        };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        var result = await _authorRepository.GetAuthorByIdAsync(authorId, trackChanges: false);

        Assert.NotNull(result);
        Assert.Equal(authorId, result.Id);
        Assert.Equal("Test", result.FirstName);
        Assert.Equal("Author", result.LastName);
    }

    [Fact]
    public async Task GetAuthorsAsync_ShouldReturnPagedList_WhenCalled()
    {
        var authors = new List<Author>
        {
            new Author { Id = Guid.NewGuid(), FirstName = "Author1", LastName = "LastName1", Country = "1" },
            new Author { Id = Guid.NewGuid(), FirstName = "Author2", LastName = "LastName2", Country = "2" }
        };

        _context.Authors.AddRange(authors);
        await _context.SaveChangesAsync();

        var authorParameters = new AuthorParameters { PageNumber = 1, PageSize = 2 };

        var result = await _authorRepository.GetAuthorsAsync(authorParameters, trackChanges: false);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateAuthor_ShouldAddAuthor()
    {
        var id = Guid.NewGuid();
        var author = new Author 
        { 
            Id = id, 
            FirstName = "New", 
            LastName = "Author",
            Country = "Country 1",
            Birthday = new DateOnly(2020, 1, 1)
        };

        _authorRepository.Create(author);
        await _context.SaveChangesAsync();

        var createdAuthor = await _context.Authors.FindAsync(id);
        Assert.NotNull(createdAuthor);
        Assert.Equal("New", createdAuthor.FirstName);
        Assert.Equal("Author", createdAuthor.LastName);
        Assert.Equal("Country 1", createdAuthor.Country);
        Assert.Equal(new DateOnly(2020, 1, 1), createdAuthor.Birthday);
    }

    [Fact]
    public async Task DeleteAuthor_ShouldRemoveAuthor()
    {
        var authorId = Guid.NewGuid();
        var author = new Author 
        { 
            Id = authorId, 
            FirstName = "ToDelete", 
            LastName = "Author",
            Country = "Country"
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        _authorRepository.Delete(author);
        await _context.SaveChangesAsync();

        var deletedAuthor = await _context.Authors.FindAsync(authorId);
        Assert.Null(deletedAuthor);
    }
}