using Library.Domain.Models;

namespace Library.Contracts;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(bool trackChanges);
    Task<Book> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<Book> GetBookByIsbnAsync(string isbn, bool trackChanges);
    void CreateBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
    void IssueBookToUser(Book book);
    void AddImageToBook();
    void SendMessageToUserAboutExpiration();
}