using AutoMapper;
using Library.Contracts;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class GetBooksByAuthorIdUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetBooksByAuthorIdUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BookDto>> ExecuteAsync(Guid id, bool trackChanges)
    {
        var books = await _repository.Book.GetBooksByAuthorIdAsync(id, trackChanges);
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return booksDto;
    }
}