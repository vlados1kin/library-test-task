using AutoMapper;
using Library.API;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Service;
using Library.Service.AuthorUseCases;
using Library.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Tests;

public class AuthorServiceTests
{
    private readonly RepositoryContext _context;
    private readonly GetAuthorsUseCase _getAuthorsUseCase;
    private readonly GetAuthorByIdUseCase _getAuthorByIdUseCase;
    private readonly CreateAuthorUseCase _createAuthorUseCase;
    private readonly UpdateAuthorUseCase _updateAuthorUseCase;
    private readonly DeleteAuthorUseCase _deleteAuthorUseCase;

    public AuthorServiceTests()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "AuthorServiceTestDatabase").Options;

        _context = new RepositoryContext(options);

        var configuration = new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        });

        var mapper = configuration.CreateMapper();

        IRepositoryManager repositoryManager = new RepositoryManager(_context);
        _getAuthorsUseCase = new GetAuthorsUseCase(repositoryManager, mapper);
        _getAuthorByIdUseCase = new GetAuthorByIdUseCase(repositoryManager, mapper);
        _createAuthorUseCase = new CreateAuthorUseCase(repositoryManager, mapper);
        _updateAuthorUseCase = new UpdateAuthorUseCase(repositoryManager, mapper);
        _deleteAuthorUseCase = new DeleteAuthorUseCase(repositoryManager);
        
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAuthorsAsync_ShouldReturnAuthorsWithMetaData()
    {
        var authorParameters = new AuthorParameters();
        var author1 = new Author { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Country = "Country 1", Birthday = new DateOnly(1980, 1, 1) };
        var author2 = new Author { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Country = "Country 2", Birthday = new DateOnly(1985, 1, 1) };

        var authors = await _context.Authors.ToListAsync();
        _context.Authors.RemoveRange(authors);
        _context.Authors.AddRange(author1, author2);
        await _context.SaveChangesAsync();

        var result = await _getAuthorsUseCase.ExecuteAsync(authorParameters, trackChanges: false);

        Assert.NotNull(result.authorDtos);
        Assert.Equal(2, result.authorDtos.Count());
    }

    [Fact]
    public async Task GetAuthorById_ShouldReturnAuthor_WhenAuthorExists()
    {
        var author = new Author { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Country = "Country 1", Birthday = new DateOnly(1980, 1, 1) };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        var result = await _getAuthorByIdUseCase.ExecuteAsync(author.Id, trackChanges: false);

        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public async Task GetAuthorById_ShouldThrowException_WhenAuthorDoesNotExist()
    {
        var authorId = Guid.NewGuid();

        await Assert.ThrowsAsync<AuthorNotFoundException>(async () => await _getAuthorByIdUseCase.ExecuteAsync(authorId, trackChanges: false));
    }

    [Fact]
    public async Task CreateAuthorAsync_ShouldAddAuthor()
    {
        var authorForCreation = new AuthorForCreationDto { FirstName = "New", LastName = "Author", Country = "Country 1", Birthday = new DateOnly(1990, 1, 1) };

        var createdAuthor = await _createAuthorUseCase.ExecuteAsync(authorForCreation);

        var authorInDb = await _context.Authors.FindAsync(createdAuthor.Id);
        Assert.NotNull(authorInDb);
        Assert.Equal("New", authorInDb.FirstName);
        Assert.Equal("Author", authorInDb.LastName);
    }

    [Fact]
    public async Task UpdateAuthorAsync_ShouldUpdateExistingAuthor()
    {
        var author = new Author { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Country = "Country 1", Birthday = new DateOnly(1980, 1, 1) };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        var authorForUpdate = new AuthorForUpdateDto { FirstName = "Updated", LastName = "Author", Country = "Updated Country", Birthday = new DateOnly(1990, 1, 1) };

        await _updateAuthorUseCase.ExecuteAsync(author.Id, authorForUpdate, trackChanges: true);

        var updatedAuthor = await _context.Authors.FindAsync(author.Id);
        Assert.NotNull(updatedAuthor);
        Assert.Equal("Updated", updatedAuthor.FirstName);
        Assert.Equal("Author", updatedAuthor.LastName);
        Assert.Equal("Updated Country", updatedAuthor.Country);
    }

    [Fact]
    public async Task DeleteAuthorAsync_ShouldRemoveAuthor()
    {
        var author = new Author { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Country = "Country 1", Birthday = new DateOnly(1980, 1, 1) };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        await _deleteAuthorUseCase.ExecuteAsync(author.Id, trackChanges: true);

        var authorInDb = await _context.Authors.FindAsync(author.Id);
        Assert.Null(authorInDb);
    }
}