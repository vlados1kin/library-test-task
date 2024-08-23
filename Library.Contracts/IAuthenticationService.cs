using Library.Shared.DTO;
using Microsoft.AspNetCore.Identity;

namespace Library.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
}