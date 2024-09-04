using Library.Contracts;
using Library.Service.IssueUseCases;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly GetIssuesByUserIdUseCase _getIssuesByUserIdUseCase;
    private readonly IUserService _userService;

    public UserController(
        IUserService userService, 
        GetIssuesByUserIdUseCase getIssuesByUserIdUseCase)
    {
        _userService = userService;
        _getIssuesByUserIdUseCase = getIssuesByUserIdUseCase;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var userDtos = await _userService.GetUsersAsync();
        return Ok(userDtos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "AdminAndSelfOnly")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        return Ok(userDto);
    }

    [HttpGet("{id:guid}/issues")]
    [Authorize(Policy = "AdminAndSelfOnly")]
    public async Task<IActionResult> GetIssuesByUserId([FromRoute] Guid id)
    {
        var issueDtos = await _getIssuesByUserIdUseCase.ExecuteAsync(id, trackChanges: false);
        return Ok(issueDtos);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        if (!await _userService.ValidateUser(userForAuthenticationDto))
            return Unauthorized();

        var tokenDto = await _userService.GenerateToken(populateExp: true);
        return Ok(tokenDto);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var result = await _userService.RegisterUser(userForRegistrationDto);
        if (result.Succeeded) 
            return StatusCode(201);
        foreach (var error in result.Errors)
            ModelState.TryAddModelError(error.Code, error.Description);
        return BadRequest(ModelState);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _userService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}