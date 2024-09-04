using System.Collections;
using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Library.Repository.Tests;

public class BookRepositoryTests
{
    private readonly RepositoryContext _context;
    private readonly BookRepository _bookRepository;

    public BookRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "Test").Options;

        _context = new RepositoryContext(options);
        _bookRepository = new BookRepository(_context);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetBooksAsync_ShouldReturnAllBooks()
    {
        var book1 = new Book { Id = Guid.NewGuid(), ISBN = "123456", Name = "Book1", AuthorId = Guid.NewGuid() };
        var book2 = new Book { Id = Guid.NewGuid(), ISBN = "654321", Name = "Book2", AuthorId = Guid.NewGuid() };
        var set = await _context.Books.ToListAsync();
        _context.Books.RemoveRange(set);
        await _context.SaveChangesAsync();
        _context.Books.AddRange(book1, book2);
        await _context.SaveChangesAsync();

        var bookParameters = new BookParameters { PageNumber = 1, PageSize = 10 };

        var result = await _bookRepository.GetBooksAsync(bookParameters, false);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, b => b.ISBN == "123456");
        Assert.Contains(result, b => b.ISBN == "654321");
    }

    [Fact]
    public async Task GetBooksByAuthorIdAsync_ShouldReturnBooksForAuthor()
    {
        var authorId = Guid.NewGuid();
        var book1 = new Book { Id = Guid.NewGuid(), ISBN = "123456", Name = "Book1", AuthorId = authorId };
        var book2 = new Book { Id = Guid.NewGuid(), ISBN = "654321", Name = "Book2", AuthorId = authorId };
        var book3 = new Book { Id = Guid.NewGuid(), ISBN = "111111", Name = "Book3", AuthorId = Guid.NewGuid() };
        _context.Books.AddRange(book1, book2, book3);
        await _context.SaveChangesAsync();

        var result = await _bookRepository.GetBooksByAuthorIdAsync(authorId, false);

        Assert.Equal(2, result.Count());
        Assert.Contains(result, b => b.ISBN == "123456");
        Assert.Contains(result, b => b.ISBN == "654321");
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnCorrectBook()
    {
        var id = Guid.NewGuid();
        var book = new Book { Id = id, ISBN = "123456", Name = "Book1", AuthorId = Guid.NewGuid() };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = await _bookRepository.GetBookByIdAsync(id, false);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("123456", result.ISBN);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnNullForNonexistentBook()
    {
        var result = await _bookRepository.GetBookByIdAsync(Guid.NewGuid(), false);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldReturnCorrectBook()
    {
        var isbn = "123456";
        var book = new Book { Id = Guid.NewGuid(), ISBN = isbn, Name = "Book1", AuthorId = Guid.NewGuid() };
        var set = await _context.Books.ToListAsync();
        _context.Books.RemoveRange(set);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = await _bookRepository.GetBookByIsbnAsync(isbn, false);

        Assert.NotNull(result);
        Assert.Equal(isbn, result.ISBN);
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldReturnNullForNonexistentIsbn()
    {
        var result = await _bookRepository.GetBookByIsbnAsync("000000", false);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateBook_ShouldAddBook()
    {
        var book = new Book { Id = Guid.NewGuid(), ISBN = "123456", Name = "New Book", AuthorId = Guid.NewGuid() };

        _bookRepository.Create(book);
        await _context.SaveChangesAsync();

        var createdBook = await _context.Books.FindAsync(book.Id);
        Assert.NotNull(createdBook);
        Assert.Equal("123456", createdBook.ISBN);
        Assert.Equal("New Book", createdBook.Name);
    }

    [Fact]
    public async Task DeleteBook_ShouldRemoveBook()
    {
        var book = new Book { Id = Guid.NewGuid(), ISBN = "123456", Name = "Book to Delete", AuthorId = Guid.NewGuid() };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        _bookRepository.Delete(book);
        await _context.SaveChangesAsync();

        var deletedBook = await _context.Books.FindAsync(book.Id);
        Assert.Null(deletedBook);
    }
}