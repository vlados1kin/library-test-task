using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Repository;
using Library.Shared.DTO;

namespace Library.Service;

public class GenreService : IGenreService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public GenreService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GenreDto>> GetGenresAsync(bool trackChanges)
    {
        var genres = await _repository.Genre.GetGenresAsync(trackChanges);
        var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genres);
        return genresDto;
    }
    
    public async Task<GenreDto> GetGenreByIdAsync(Guid id, bool trackChanges)
    {
        var genre = await _repository.Genre.GetGenreByIdAsync(id, trackChanges);
        if (genre is null)
            throw new GenreNotFoundException(id);
        var genreDto = _mapper.Map<GenreDto>(genre);
        return genreDto;
    }

    public async Task<GenreDto> CreateGenreAsync(GenreForCreationDto genreForCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreForCreationDto);
        _repository.Genre.Create(genre);
        await _repository.SaveAsync();
        var genreDto = _mapper.Map<GenreDto>(genre);
        return genreDto;
    }

    public async Task UpdateGenreAsync(Guid id, GenreForUpdateDto genreForUpdateDto, bool trackChanges)
    {
        var genre = await _repository.Genre.GetGenreByIdAsync(id, trackChanges);
        if (genre is null)
            throw new GenreNotFoundException(id);
        _mapper.Map(genreForUpdateDto, genre);
        await _repository.SaveAsync();
    }

    public async Task DeleteGenreAsync(Guid id, bool trackChanges)
    {
        var genre = await _repository.Genre.GetGenreByIdAsync(id, trackChanges);
        if (genre is null)
            throw new GenreNotFoundException(id);
        _repository.Genre.Delete(genre);
        await _repository.SaveAsync();
    }
}