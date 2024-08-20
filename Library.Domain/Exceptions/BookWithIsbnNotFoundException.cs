namespace Library.Domain.Exceptions;

public class BookWithIsbnNotFoundException : NotFoundException
{
    public BookWithIsbnNotFoundException(string isbn) : base($"The book with ISBN: {isbn} does not exist in the database.")
    {
    }
}