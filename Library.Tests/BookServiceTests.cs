using AutoMapper;
using Library.API;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Service.BookUseCases;
using Library.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Tests;

public class BookServiceTests
{
    private readonly RepositoryContext _context;
    private readonly GetBooksUseCase _getBooksUseCase;
    private readonly GetBookByIdUseCase _getBookByIdUseCase;
    private readonly GetBookByIsbnUseCase _getBookByIsbnUseCase;
    private readonly GetBooksByAuthorIdUseCase _getBooksByAuthorIdUseCase;
    private readonly CreateBookUseCase _createBookUseCase;
    private readonly UpdateBookUseCase _updateBookUseCase;
    private readonly DeleteBookUseCase _deleteBookUseCase;

    public BookServiceTests()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "Test")
            .Options;

        _context = new RepositoryContext(options);

        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

        var mapper = configuration.CreateMapper();

        IRepositoryManager repositoryManager = new RepositoryManager(_context);

        _getBooksUseCase = new GetBooksUseCase(repositoryManager, mapper);
        _getBookByIdUseCase = new GetBookByIdUseCase(repositoryManager, mapper);
        _getBookByIsbnUseCase = new GetBookByIsbnUseCase(repositoryManager, mapper);
        _getBooksByAuthorIdUseCase = new GetBooksByAuthorIdUseCase(repositoryManager, mapper);
        _createBookUseCase = new CreateBookUseCase(repositoryManager, mapper);
        _updateBookUseCase = new UpdateBookUseCase(repositoryManager, mapper);
        _deleteBookUseCase = new DeleteBookUseCase(repositoryManager);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetBooksAsync_ShouldReturnBooksWithMetaData()
    {
        var bookParameters = new BookParameters();
        var book1 = new Book
        {
            Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };
        var book2 = new Book
        {
            Id = Guid.NewGuid(), ISBN = "0987654321", Name = "Book 2", AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };

        var books = await _context.Books.ToListAsync();
        _context.Books.RemoveRange(books);
        _context.Books.AddRange(book1, book2);
        await _context.SaveChangesAsync();

        var result = await _getBooksUseCase.ExecuteAsync(bookParameters, trackChanges: false);

        Assert.NotNull(result.bookDtos);
        Assert.Equal(2, result.bookDtos.Count());
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = await _getBookByIdUseCase.ExecuteAsync(book.Id, trackChanges: false);

        Assert.NotNull(result);
        Assert.Equal("Book 1", result.Name);
        Assert.Equal("1234567890", result.ISBN);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldThrowException_WhenBookDoesNotExist()
    {
        var bookId = Guid.NewGuid();

        await Assert.ThrowsAsync<BookWithIdNotFoundException>(async () =>
            await _getBookByIdUseCase.ExecuteAsync(bookId, trackChanges: false));
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldReturnBook_WhenBookExists()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = await _getBookByIsbnUseCase.ExecuteAsync("1234567890", trackChanges: false);

        Assert.NotNull(result);
        Assert.Equal("Book 1", result.Name);
        Assert.Equal("1234567890", result.ISBN);
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldThrowException_WhenBookDoesNotExist()
    {
        var isbn = "1111111111";

        await Assert.ThrowsAsync<BookWithIsbnNotFoundException>(async () =>
            await _getBookByIsbnUseCase.ExecuteAsync(isbn, trackChanges: false));
    }

    [Fact]
    public async Task CreateBookAsync_ShouldAddBook()
    {
        var bookForCreation = new BookForCreationDto
            { ISBN = "1234567890", Name = "New Book", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        var createdBook = await _createBookUseCase.ExecuteAsync(bookForCreation);

        var bookInDb = await _context.Books.FindAsync(createdBook.Id);
        Assert.NotNull(bookInDb);
        Assert.Equal("New Book", bookInDb.Name);
        Assert.Equal("1234567890", bookInDb.ISBN);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateExistingBook()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Old Book", AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var bookForUpdate = new BookForUpdateDto
            { ISBN = "0987654321", Name = "Updated Book", AuthorId = book.AuthorId, GenreId = book.GenreId };

        await _updateBookUseCase.ExecuteAsync(book.Id, bookForUpdate, trackChanges: true);

        var updatedBook = await _context.Books.FindAsync(book.Id);
        Assert.NotNull(updatedBook);
        Assert.Equal("Updated Book", updatedBook.Name);
        Assert.Equal("0987654321", updatedBook.ISBN);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldRemoveBook()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book to Delete", AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        await _deleteBookUseCase.ExecuteAsync(book.Id, trackChanges: true);

        var bookInDb = await _context.Books.FindAsync(book.Id);
        Assert.Null(bookInDb);
    }

    [Fact]
    public async Task GetBooksByAuthorIdAsync_ShouldReturnBooksByAuthor()
    {
        var authorId = Guid.NewGuid();
        var book1 = new Book
        {
            Id = Guid.NewGuid(), ISBN = "1234567890", Name = "Book 1", AuthorId = authorId, GenreId = Guid.NewGuid()
        };
        var book2 = new Book
        {
            Id = Guid.NewGuid(), ISBN = "0987654321", Name = "Book 2", AuthorId = authorId, GenreId = Guid.NewGuid()
        };

        _context.Books.AddRange(book1, book2);
        await _context.SaveChangesAsync();

        var result = await _getBooksByAuthorIdUseCase.ExecuteAsync(authorId, trackChanges: false);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}