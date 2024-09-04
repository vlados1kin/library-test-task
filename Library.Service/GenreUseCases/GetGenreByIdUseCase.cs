using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.GenreUseCases;

public class GetGenreByIdUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public GetGenreByIdUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<GenreDto> ExecuteAsync(Guid id, bool trackChanges)
    {
        var genre = await _repository.Genre.GetGenreByIdAsync(id, trackChanges);
        if (genre is null)
            throw new GenreNotFoundException(id);
        var genreDto = _mapper.Map<GenreDto>(genre);
        return genreDto;
    }
}