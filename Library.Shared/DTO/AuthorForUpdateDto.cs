namespace Library.Shared.DTO;

public record AuthorForUpdateDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateOnly Birthday { get; init; }
    public string Country { get; init; }
}