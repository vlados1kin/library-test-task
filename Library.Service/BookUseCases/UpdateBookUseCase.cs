using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class UpdateBookUseCase
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public UpdateBookUseCase(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task ExecuteAsync(Guid id, BookForUpdateDto bookForUpdateDto, bool trackChanges)
    {
        var book = await _repository.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        _mapper.Map(bookForUpdateDto, book);
        await _repository.SaveChangesAsync();
    }
}