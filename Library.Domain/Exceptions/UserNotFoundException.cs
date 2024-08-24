namespace Library.Domain.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid id) : base($"The user with id: {id} does not exist in the database.")
    {
    }
}