namespace Library.Shared.DTO;

public record UserDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? UserName { get; init; }
    public string? PasswordHash { get; init; }
}