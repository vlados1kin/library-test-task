using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges) =>
        await FindAll(trackChanges)
            .Skip((authorParameters.PageNumber - 1) * authorParameters.PageSize)
            .Take(authorParameters.PageSize)
            .ToListAsync();

    public async Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void CreateAuthor(Author author) => Create(author);

    public void DeleteAuthor(Author author) => Delete(author);
}