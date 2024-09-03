using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class CreateBookUseCase
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public CreateBookUseCase(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BookDto> ExecuteAsync(BookForCreationDto bookForCreationDto)
    {
        var book = _mapper.Map<Book>(bookForCreationDto);
        _repository.Create(book);
        await _repository.SaveChangesAsync();
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }
}