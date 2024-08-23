using System.ComponentModel.DataAnnotations;

namespace Library.Shared.DTO;

public record UserForRegistrationDto
{
    [Required(ErrorMessage = "Firstname is required.")]
    public string? FirstName { get; init; }
    [Required(ErrorMessage = "Lastname is required.")]
    public string? LastName { get; init; }
    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; init; }
    public ICollection<string>? Roles { get; init; }
}