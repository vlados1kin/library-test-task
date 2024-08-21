using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.DTO;

namespace Library.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<BookDto> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<BookDto> GetBookByIsbnAsync(string isbn, bool trackChanges);
    Task<BookDto> CreateBookAsync(BookForCreationDto bookForCreationDto);
    Task UpdateBookAsync(Guid id, BookForUpdateDto bookForUpdateDto, bool trackChanges);
    Task DeleteBookAsync(Guid id, bool trackChanges);
    Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(Guid id, bool trackChanges);
    Task IssueBookAsync(Guid id, BookForIssueDto bookForUpdateDto, bool trackChanges);
    // TODO: выдать книгу пользователю
    // TODO: добавление изображения к книге
    // TODO: отправка уведомления об истечении срока выдачи книги
}