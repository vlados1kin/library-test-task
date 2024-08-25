using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<Issue>? Issues { get; set; }
}