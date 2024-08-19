using Library.Shared.DTO;

namespace Library.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync(bool trackChanges);
    Task<AuthorDto> GetAuthorById(Guid id, bool trackChanges);
    Task CreateAuthorAsync(AuthorForCreationDto authorForCreationDto);
    Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto authorForUpdateDto, bool trackChanges);
    Task DeleteAuthorAsync(Guid id, bool trackChanges);
}