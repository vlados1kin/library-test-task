using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.RequestFeatures;

namespace Library.Contracts;

public interface IAuthorRepository : IRepositoryBase<Author>
{
    Task<PagedList<Author>> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges);
    Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges);
}