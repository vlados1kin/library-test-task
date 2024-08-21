using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<PagedList<Author>> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges)
    {
        var authors = await FindAll(trackChanges).ToListAsync();
        return PagedList<Author>.ToPagedList(authors, authorParameters.PageNumber, authorParameters.PageSize);
    }
        

    public async Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void CreateAuthor(Author author) => Create(author);

    public void DeleteAuthor(Author author) => Delete(author);
}