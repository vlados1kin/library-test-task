using AutoMapper;
using Library.API;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Service;
using Library.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Tests;

public class BookServiceTests
{
    private readonly RepositoryContext _context;
    private readonly IBookService _bookService;

    public BookServiceTests()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "Test").Options;

        _context = new RepositoryContext(options);

        var configuration = new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        });

        var mapper = configuration.CreateMapper();

        IRepositoryManager repositoryManager = new RepositoryManager(_context);

        _bookService = new BookService(repositoryManager, mapper);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetBooksAsync_ShouldReturnBooksWithMetaData()
    {
        // Arrange
        var bookParameters = new BookParameters();
        var book1 = new Book { Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };
        var book2 = new Book { Id = Guid.NewGuid(), ISBN = "0987654321", Name = "Book 2", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        var books = await _context.Books.ToListAsync();
        _context.Books.RemoveRange(books);
        _context.Books.AddRange(book1, book2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _bookService.GetBooksAsync(bookParameters, trackChanges: false);

        // Assert
        Assert.NotNull(result.bookDtos);
        Assert.Equal(2, result.bookDtos.Count());
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        var result = await _bookService.GetBookByIdAsync(book.Id, trackChanges: false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Book 1", result.Name);
        Assert.Equal("1234567890", result.ISBN);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<BookWithIdNotFoundException>(async () =>
            await _bookService.GetBookByIdAsync(bookId, trackChanges: false));
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        var result = await _bookService.GetBookByIsbnAsync("1234567890", trackChanges: false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Book 1", result.Name);
        Assert.Equal("1234567890", result.ISBN);
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        var isbn = "1111111111";

        // Act & Assert
        await Assert.ThrowsAsync<BookWithIsbnNotFoundException>(async () =>
            await _bookService.GetBookByIsbnAsync(isbn, trackChanges: false));
    }

    [Fact]
    public async Task CreateBookAsync_ShouldAddBook()
    {
        // Arrange
        var bookForCreation = new BookForCreationDto { ISBN = "1234567890", Name = "New Book", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        // Act
        var createdBook = await _bookService.CreateBookAsync(bookForCreation);

        // Assert
        var bookInDb = await _context.Books.FindAsync(createdBook.Id);
        Assert.NotNull(bookInDb);
        Assert.Equal("New Book", bookInDb.Name);
        Assert.Equal("1234567890", bookInDb.ISBN);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateExistingBook()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Old Book", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var bookForUpdate = new BookForUpdateDto { ISBN = "0987654321", Name = "Updated Book", AuthorId = book.AuthorId, GenreId = book.GenreId };

        // Act
        await _bookService.UpdateBookAsync(book.Id, bookForUpdate, trackChanges: true);

        // Assert
        var updatedBook = await _context.Books.FindAsync(book.Id);
        Assert.NotNull(updatedBook);
        Assert.Equal("Updated Book", updatedBook.Name);
        Assert.Equal("0987654321", updatedBook.ISBN);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldRemoveBook()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book to Delete", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        await _bookService.DeleteBookAsync(book.Id, trackChanges: true);

        // Assert
        var bookInDb = await _context.Books.FindAsync(book.Id);
        Assert.Null(bookInDb);
    }

    [Fact]
    public async Task GetBooksByAuthorIdAsync_ShouldReturnBooksByAuthor()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var book1 = new Book { Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = authorId, GenreId = Guid.NewGuid() };
        var book2 = new Book { Id = Guid.NewGuid(), ISBN = "0987654321", Name = "Book 2", AuthorId = authorId, GenreId = Guid.NewGuid() };

        _context.Books.AddRange(book1, book2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _bookService.GetBooksByAuthorIdAsync(authorId, trackChanges: false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}