namespace Library.Shared.DTO;

public record TokenDto
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
