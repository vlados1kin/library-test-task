using Library.Contracts;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync(bool trackChanges) =>
        await FindAll(trackChanges).ToListAsync();

    public async Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void CreateAuthor(Author author) => Create(author);

    public void DeleteAuthor(Author author) => Delete(author);

    public async Task<Author> GetAuthorByIdWithBooksAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(id), trackChanges)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genre)
            .SingleOrDefaultAsync();
}