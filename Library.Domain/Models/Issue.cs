namespace Library.Domain.Models;

public class Issue
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ReceiveTime { get; set; }
    public DateTime ReturnTime { get; set; }
    
    public Book Book { get; set; }
    public User User { get; set; }
}