using Library.Domain.Models;

namespace Library.Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync(bool trackChanges);
    Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges);
    void CreateAuthor(Author author);
    void DeleteAuthor(Author author);
}