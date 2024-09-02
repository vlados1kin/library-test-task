using Library.Domain.Models;

namespace Library.Contracts;

public interface IGenreRepository : IRepositoryBase<Genre>
{
    Task<IEnumerable<Genre>> GetGenresAsync(bool trackChanges);
    Task<Genre> GetGenreByIdAsync(Guid id, bool trackChanges);
}