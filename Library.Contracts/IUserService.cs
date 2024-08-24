using Library.Shared.DTO;
using Microsoft.AspNetCore.Identity;

namespace Library.Contracts;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto);
    Task<string> GenerateToken();
}