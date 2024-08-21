using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Service;

public class AuthorService : IAuthorService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public AuthorService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<(IEnumerable<AuthorDto> authorDtos, MetaData metaData)> GetAuthorsAsync(AuthorParameters authorParameters, bool trackChanges)
    {
        var authorsWithMetaData = await _repository.Author.GetAuthorsAsync(authorParameters, trackChanges);
        var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authorsWithMetaData);
        return (authorDtos: authorsDto, metaData: authorsWithMetaData.MetaData);
    }

    public async Task<AuthorDto> GetAuthorById(Guid id, bool trackChanges)
    {
        var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        var authorDto = _mapper.Map<AuthorDto>(author);
        return authorDto;
    }

    public async Task<AuthorDto> CreateAuthorAsync(AuthorForCreationDto authorForCreationDto)
    {
        var author = _mapper.Map<Author>(authorForCreationDto);
        _repository.Author.CreateAuthor(author);
        await _repository.SaveAsync();
        var authorDto = _mapper.Map<AuthorDto>(author);
        return authorDto;
    }

    public async Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto authorForUpdateDto, bool trackChanges)
    {
        var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        _mapper.Map(authorForUpdateDto, author);
        await _repository.SaveAsync();
    }

    public async Task DeleteAuthorAsync(Guid id, bool trackChanges)
    {
        var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);
        _repository.Author.DeleteAuthor(author);
        await _repository.SaveAsync();
    }
}