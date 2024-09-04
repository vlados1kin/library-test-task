using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.GenreUseCases;

public class UpdateGenreUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public UpdateGenreUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task ExecuteAsync(Guid id, GenreForUpdateDto genreForUpdateDto, bool trackChanges)
    {
        var genre = await _repository.Genre.GetGenreByIdAsync(id, trackChanges);
        if (genre is null)
            throw new GenreNotFoundException(id);
        _mapper.Map(genreForUpdateDto, genre);
        await _repository.SaveAsync();
    }
}