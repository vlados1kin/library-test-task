namespace Library.Domain.Exceptions;

public class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException(Guid id) : base($"The author with id: {id} does not exist in the database.")
    {
    }
}