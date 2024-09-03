using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Service.AuthorUseCases;

public class GetAuthorByIdUseCase
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public GetAuthorByIdUseCase(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AuthorDto> ExecuteAsync(Guid id, bool trackChanges)
    {
        var author = await _repository.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        var authorDto = _mapper.Map<AuthorDto>(author);
        return authorDto;
    }
}