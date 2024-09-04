using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;

namespace Library.Service.GenreUseCases;

public class DeleteGenreUseCase
{
    private readonly IRepositoryManager _repository;
    
    public DeleteGenreUseCase(IRepositoryManager repository) => _repository = repository;
    
    public async Task ExecuteAsync(Guid id, bool trackChanges)
    {
        var genre = await _repository.Genre.GetGenreByIdAsync(id, trackChanges);
        if (genre is null)
            throw new GenreNotFoundException(id);
        _repository.Genre.Delete(genre);
        await _repository.SaveAsync();
    }
}