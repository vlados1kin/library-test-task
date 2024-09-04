using AutoMapper;
using Library.Contracts;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service.AuthorUseCases;

public class CreateAuthorUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public CreateAuthorUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<AuthorDto> ExecuteAsync(AuthorForCreationDto authorForCreationDto)
    {
        var author = _mapper.Map<Author>(authorForCreationDto);
        _repository.Author.Create(author);
        await _repository.SaveAsync();
        var authorDto = _mapper.Map<AuthorDto>(author);
        return authorDto;
    }
}