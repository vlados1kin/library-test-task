namespace Library.Shared.DTO;

public record GenreDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}