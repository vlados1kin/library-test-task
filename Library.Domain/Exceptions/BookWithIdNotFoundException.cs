namespace Library.Domain.Exceptions;

public class BookWithIdNotFoundException : NotFoundException
{
    public BookWithIdNotFoundException(Guid id) : base($"The book with id: {id} does not exist in the database.")
    {
    }
}