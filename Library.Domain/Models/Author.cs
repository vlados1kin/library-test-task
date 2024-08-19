namespace Library.Domain.Models;

public class Author
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public string Country { get; set; }
    
    public ICollection<Book> Books { get; set; }
}