using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;

namespace Library.Service.AuthorUseCases;

public class DeleteAuthorUseCase
{
    private readonly IRepositoryManager _repository;

    public DeleteAuthorUseCase(IRepositoryManager repository) => _repository = repository;
    
    public async Task ExecuteAsync(Guid id, bool trackChanges)
    {
        var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        _repository.Author.Delete(author);
        await _repository.SaveAsync();
    }
}