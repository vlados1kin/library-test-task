using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service.GenreUseCases;

public class CreateGenreUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public CreateGenreUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<GenreDto> ExecuteAsync(GenreForCreationDto genreForCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreForCreationDto);
        _repository.Genre.Create(genre);
        await _repository.SaveAsync();
        var genreDto = _mapper.Map<GenreDto>(genre);
        return genreDto;
    }
}