using Library.Shared.DTO;

namespace Library.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetBooksAsync(bool trackChanges);
    Task<BookDto> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<BookDto> GetBookByIsbnAsync(string isbn, bool trackChanges);
    Task<BookDto> CreateBookAsync(BookForCreationDto bookForCreationDto);
    Task UpdateBookAsync(Guid id, BookForUpdateDto bookForUpdateDto, bool trackChanges);
    Task DeleteBookAsync(Guid id, bool trackChanges);
    Task IssueBookToUser(Guid id, BookForIssueDto bookForIssueDto, bool trackChanges);
    // TODO: добавление изображения к книге
    // TODO: отправка уведомления об истечении срока выдачи книги
}