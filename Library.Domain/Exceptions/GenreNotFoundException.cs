namespace Library.Domain.Exceptions;

public class GenreNotFoundException : NotFoundException
{
    public GenreNotFoundException(Guid id) : base($"The genre with id: {id} does not exist in the database.")
    {
    }
}