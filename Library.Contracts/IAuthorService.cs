using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Contracts;

public interface IAuthorService
{
    Task<(IEnumerable<AuthorDto> authorDtos, MetaData metaData)> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges);
    Task<AuthorDto> GetAuthorById(Guid id, bool trackChanges);
    Task<AuthorDto> CreateAuthorAsync(AuthorForCreationDto authorForCreationDto);
    Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto authorForUpdateDto, bool trackChanges);
    Task DeleteAuthorAsync(Guid id, bool trackChanges);
}