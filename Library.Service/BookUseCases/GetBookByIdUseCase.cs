using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class GetBookByIdUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetBookByIdUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BookDto> ExecuteAsync(Guid id, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }
}