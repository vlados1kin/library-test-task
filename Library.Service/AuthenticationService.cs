using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Library.Contracts;
using Library.Domain.Models;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    private User? _user;
    
    public AuthenticationService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
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