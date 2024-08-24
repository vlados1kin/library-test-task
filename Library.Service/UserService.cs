using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.Service;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    private User? _user;
    
    public UserService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return userDtos;
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _userManager.Users.Where(u => u.Id.Equals(id)).SingleOrDefaultAsync();
        if (user is null)
            throw new UserNotFoundException(id);
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
    
    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
    {
        _user = await _userManager.FindByNameAsync(userForAuthenticationDto.UserName);
        return _user != null && await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.Password);
    }

    public async Task<string> GenerateToken()
    {
        var key = _configuration["JwtSettings:SecretKey"];
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, _user.UserName) };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()));
        
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}