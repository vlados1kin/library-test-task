using Library.Contracts;
using Library.Domain.Models;
using Library.Repository;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{
    public GenreRepository(RepositoryContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Genre>> GetGenresAsync(bool trackChanges)
        => await FindAll(trackChanges).ToListAsync();

    public async Task<Genre> GetGenreByIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void CreateGenre(Genre genre) => Create(genre);

    public void DeleteGenre(Genre genre) => Delete(genre);
}