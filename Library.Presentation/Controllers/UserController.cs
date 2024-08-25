using Library.Contracts;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IServiceManager _service;

    public UserController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var userDtos = await _service.UserService.GetUsersAsync();
        return Ok(userDtos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "AdminAndSelfOnly")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var userDto = await _service.UserService.GetUserByIdAsync(id);
        return Ok(userDto);
    }

    [HttpGet("{id:guid}/issues")]
    [Authorize(Policy = "AdminAndSelfOnly")]
    public async Task<IActionResult> GetIssuesByUserId([FromRoute] Guid id)
    {
        var issueDtos = await _service.IssueService.GetIssuesByUserIdAsync(id, trackChanges: false);
        return Ok(issueDtos);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        if (!await _service.UserService.ValidateUser(userForAuthenticationDto))
            return Unauthorized();

        var tokenDto = await _service.UserService.GenerateToken(populateExp: true);
        return Ok(tokenDto);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var result = await _service.UserService.RegisterUser(userForRegistrationDto);
        if (result.Succeeded) 
            return StatusCode(201);
        foreach (var error in result.Errors)
            ModelState.TryAddModelError(error.Code, error.Description);
        return BadRequest(ModelState);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.UserService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}