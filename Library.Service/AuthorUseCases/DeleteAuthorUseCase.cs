using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;

namespace Library.Service.AuthorUseCases;

public class DeleteAuthorUseCase
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public DeleteAuthorUseCase(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task ExecuteAsync(Guid id, bool trackChanges)
    {
        var author = await _repository.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        _repository.Delete(author);
        await _repository.SaveAsync();
    }
}