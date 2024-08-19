using Library.Contracts;
using Library.Shared.DTO;

namespace Library.Service;

public class AuthorService : IAuthorService
{
    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync(bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDto> GetAuthorById(Guid id, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAuthorAsync(AuthorForCreationDto authorForCreationDto)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto authorForUpdateDto, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAuthorAsync(Guid id, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}