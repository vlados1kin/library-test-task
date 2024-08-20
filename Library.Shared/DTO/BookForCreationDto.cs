namespace Library.Shared.DTO;

public record BookForCreationDto
{
    public string ISBN { get; set; }
    public string Name { get; set; }
    public Guid? GenreId { get; set; }
    public string? Title { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime ReceiveTime { get; set; }
    public DateTime ReturnTime { get; set; }
}