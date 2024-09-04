using AutoMapper;
using Library.Contracts;
using Library.Shared.DTO;

namespace Library.Service.GenreUseCases;

public class GetGenresUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public GetGenresUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GenreDto>> ExecuteAsync(bool trackChanges)
    {
        var genres = await _repository.Genre.GetGenresAsync(trackChanges);
        var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genres);
        return genresDto;
    }
}