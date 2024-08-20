using Library.Contracts;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthorController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetAuthors")]
    public async Task<IActionResult> GetAuthors()
    {
        var authorDto = await _service.AuthorService.GetAuthorsAsync(trackChanges: false);
        return Ok(authorDto);
    }

    [HttpGet("{id:guid}", Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthorById([FromRoute] Guid id)
    {
        var authorDto = await _service.AuthorService.GetAuthorById(id, trackChanges: false);
        return Ok(authorDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorForCreationDto authorForCreationDto)
    {
        var authorDto = await _service.AuthorService.CreateAuthorAsync(authorForCreationDto);
        return CreatedAtRoute("GetAuthorById", new { id = authorDto.Id }, authorDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAuthor([FromRoute] Guid id, [FromBody] AuthorForUpdateDto authorForUpdateDto)
    {
        await _service.AuthorService.UpdateAuthorAsync(id, authorForUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor([FromRoute] Guid id)
    {
        await _service.AuthorService.DeleteAuthorAsync(id, trackChanges: true);
        return NoContent();
    }
}