using Library.Domain.Models;

namespace Library.Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync(bool trackChanges);
    Task<Author> GetAuthorByIdAsync(bool trackChanges);
    void CreateAuthor(Author author);
    void DeleteAuthor(Guid id);
}