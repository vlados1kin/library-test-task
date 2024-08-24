namespace Library.Domain.Exceptions;

public class IssueNotFoundException : NotFoundException
{
    public IssueNotFoundException(Guid id) : base($"The issue with id: {id} does not exist in the database.")
    {
    }
}