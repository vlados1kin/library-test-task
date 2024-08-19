namespace Library.Domain.Models;

public class Book
{
    public Guid Id { get; set; }
    public string ISBN { get; set; }
    public string Name { get; set; }
    public Guid? GenreId { get; set; }
    public string? Title { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime ReceiveTime { get; set; }
    public DateTime ReturnTime { get; set; }
    
    public Genre? Genre { get; set; }
    public Author Author { get; set; }
}