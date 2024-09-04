using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.BookUseCases;

public class UpdateBookUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public UpdateBookUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task ExecuteAsync(Guid id, BookForUpdateDto bookForUpdateDto, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        _mapper.Map(bookForUpdateDto, book);
        await _repository.SaveAsync();
    }
}