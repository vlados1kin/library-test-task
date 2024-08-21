using Library.Domain.Settings;
using Library.Shared.DTO;

namespace Library.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges);
    Task<AuthorDto> GetAuthorById(Guid id, bool trackChanges);
    Task<AuthorDto> CreateAuthorAsync(AuthorForCreationDto authorForCreationDto);
    Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto authorForUpdateDto, bool trackChanges);
    Task DeleteAuthorAsync(Guid id, bool trackChanges);
}