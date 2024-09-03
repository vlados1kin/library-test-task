using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class GetBooksByAuthorIdUseCase
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public GetBooksByAuthorIdUseCase(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(Guid id, bool trackChanges)
    {
        var books = await _repository.GetBooksByAuthorIdAsync(id, trackChanges);
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return booksDto;
    }
}