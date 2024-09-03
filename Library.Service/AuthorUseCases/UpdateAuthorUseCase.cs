using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.AuthorUseCases;

public class UpdateAuthorUseCase
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public UpdateAuthorUseCase(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task ExecuteAsync(Guid id, AuthorForUpdateDto authorForUpdateDto, bool trackChanges)
    {
        var author = await _repository.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        _mapper.Map(authorForUpdateDto, author);
        await _repository.SaveAsync();
    }
}