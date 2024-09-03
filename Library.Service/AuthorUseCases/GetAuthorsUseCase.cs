using AutoMapper;
using Library.Contracts;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Service.AuthorUseCases;

public class GetAuthorsUseCase
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public GetAuthorsUseCase(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<(IEnumerable<AuthorDto> authorDtos, MetaData metaData)> ExecuteAsync(AuthorParameters authorParameters, bool trackChanges)
    {
        var authorsWithMetaData = await _repository.GetAuthorsAsync(authorParameters, trackChanges);
        var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authorsWithMetaData);
        return (authorDtos: authorsDto, metaData: authorsWithMetaData.MetaData);
    }
}