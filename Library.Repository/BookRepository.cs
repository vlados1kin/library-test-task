using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<PagedList<Book>> GetBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
        var books = await FindAll(trackChanges).ToListAsync();
        return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId, bool trackChanges)
        => await FindByCondition(b => b.AuthorId.Equals(authorId), trackChanges).ToListAsync();

    public async Task<Book> GetBookByIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public async Task<Book> GetBookByIsbnAsync(string isbn, bool trackChanges)
        => await FindByCondition(b => b.ISBN.Equals(isbn), trackChanges).SingleOrDefaultAsync();
}