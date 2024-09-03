using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class GetBookByIsbnUseCase
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public GetBookByIsbnUseCase(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BookDto> ExecuteAsync(string isbn, bool trackChanges)
    {
        var book = await _repository.GetBookByIsbnAsync(isbn, trackChanges);
        if (book is null)
            throw new BookWithIsbnNotFoundException(isbn);
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }
}