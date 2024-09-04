using Library.Contracts;
using Library.Domain.Exceptions;

namespace Library.Service.BookUseCases;

public class DeleteBookUseCase
{
    private readonly IRepositoryManager _repository;

    public DeleteBookUseCase(IRepositoryManager repository) => _repository = repository;
    
    public async Task ExecuteAsync(Guid id, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        _repository.Book.Delete(book);
        await _repository.SaveAsync();
    }
}