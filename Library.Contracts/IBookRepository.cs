using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.RequestFeatures;

namespace Library.Contracts;

public interface IBookRepository : IRepositoryBase<Book>
{
    Task<PagedList<Book>> GetBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId, bool trackChanges);
    Task<Book> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<Book> GetBookByIsbnAsync(string isbn, bool trackChanges);
    Task SaveChangesAsync();
}