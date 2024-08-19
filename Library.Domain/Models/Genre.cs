namespace Library.Domain.Models;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Book> Books { get; set; }
}