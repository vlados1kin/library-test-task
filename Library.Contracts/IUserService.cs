using Library.Shared.DTO;
using Microsoft.AspNetCore.Identity;

namespace Library.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto);
    Task<TokenDto> GenerateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}