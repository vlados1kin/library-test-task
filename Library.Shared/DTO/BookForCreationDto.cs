namespace Library.Shared.DTO;

public record BookForCreationDto
{
    public string ISBN { get; init; }
    public string Name { get; init; }
    public Guid? GenreId { get; init; }
    public string? Title { get; init; }
    public Guid AuthorId { get; init; }
    public DateTime ReceiveTime { get; init; }
    public DateTime ReturnTime { get; init; }
}