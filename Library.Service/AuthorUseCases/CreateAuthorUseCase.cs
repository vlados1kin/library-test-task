using AutoMapper;
using Library.Contracts;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service.AuthorUseCases;

public class CreateAuthorUseCase
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public CreateAuthorUseCase(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<AuthorDto> ExecuteAsync(AuthorForCreationDto authorForCreationDto)
    {
        var author = _mapper.Map<Author>(authorForCreationDto);
        _repository.Create(author);
        await _repository.SaveChangesAsync();
        var authorDto = _mapper.Map<AuthorDto>(author);
        return authorDto;
    }
}