using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    private readonly RepositoryContext _context;
    
    public AuthorRepository(RepositoryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Author>> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges)
    {
        var authors = await FindAll(trackChanges).ToListAsync();
        return PagedList<Author>.ToPagedList(authors, authorParameters.PageNumber, authorParameters.PageSize);
    }
    
    public async Task<Author> GetAuthorByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}