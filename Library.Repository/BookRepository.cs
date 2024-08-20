﻿using Library.Contracts;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(bool trackChanges)
        => await FindAll(trackChanges).ToListAsync();

    public async Task<Book> GetBookByIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public async Task<Book> GetBookByIsbnAsync(string isbn, bool trackChanges)
        => await FindByCondition(b => b.ISBN.Equals(isbn), trackChanges).SingleOrDefaultAsync();

    public void CreateBook(Book book) => Create(book);

    public void UpdateBook(Book book) => Update(book);

    public void DeleteBook(Book book) => Delete(book);

    public void IssueBookToUser(Book book) // PATCH
    {
        throw new NotImplementedException();
    }

    public void AddImageToBook() // ???
    {
        throw new NotImplementedException();
    }

    public void SendMessageToUserAboutExpiration() // ???
    {
        throw new NotImplementedException();
    }
}