using AutoMapper;
using Library.Contracts;
using Library.Shared.DTO;

namespace Library.Service;

public class AuthorService : IAuthorService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public AuthorService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
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