using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Contracts;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetGenresAsync(bool trackChanges);
    Task<GenreDto> GetGenreByIdAsync(Guid id, bool trackChanges);
    Task<GenreDto> CreateGenreAsync(GenreForCreationDto genreForCreationDto);
    Task UpdateGenreAsync(Guid id, GenreForUpdateDto genreForUpdateDto, bool trackChanges);
    Task DeleteGenreAsync(Guid id, bool trackChanges);
}