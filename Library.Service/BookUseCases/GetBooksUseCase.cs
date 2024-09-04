using AutoMapper;
using Library.Contracts;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Service.BookUseCases;

public class GetBooksUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetBooksUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<(IEnumerable<BookDto> bookDtos, MetaData metaData)> ExecuteAsync(BookParameters bookParameters, bool trackChanges)
    {
        var booksWithMetaData = await _repository.Book.GetBooksAsync(bookParameters, trackChanges);
        var bookDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
        return (bookDtos: bookDto, metaData: booksWithMetaData.MetaData);
    }
}