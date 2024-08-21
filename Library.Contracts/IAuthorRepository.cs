using Library.Domain.Models;
using Library.Domain.Settings;

namespace Library.Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges);
    Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges);
    void CreateAuthor(Author author);
    void DeleteAuthor(Author author);
}