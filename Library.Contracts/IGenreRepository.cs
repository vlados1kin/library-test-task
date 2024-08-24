using Library.Domain.Models;

namespace Library.Contracts;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetGenresAsync(bool trackChanges);
    Task<Genre> GetGenreByIdAsync(Guid id, bool trackChanges);
    void CreateGenre(Genre genre);
    void DeleteGenre(Genre genre);
}