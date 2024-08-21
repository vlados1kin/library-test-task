using Library.Domain.Models;
using Library.Domain.Settings;

namespace Library.Contracts;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId, bool trackChanges);
    Task<Book> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<Book> GetBookByIsbnAsync(string isbn, bool trackChanges);
    void CreateBook(Book book);
    void DeleteBook(Book book);
}