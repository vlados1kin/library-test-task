using AutoMapper;
using Library.Contracts;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class CreateBookUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public CreateBookUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BookDto> ExecuteAsync(BookForCreationDto bookForCreationDto)
    {
        var book = _mapper.Map<Book>(bookForCreationDto);
        _repository.Book.Create(book);
        await _repository.SaveAsync();
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }
}