using Library.Contracts;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<IActionResult> GetAuthors([FromQuery] AuthorParameters authorParameters)
    {
        var authorDtosWithMetaData = await _service.AuthorService.GetAuthorsAsync(authorParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(authorDtosWithMetaData.metaData));
        return Ok(authorDtosWithMetaData.authorDtos);
    }

    [HttpGet("{id:guid}", Name = "GetAuthorById")]
    [Authorize(Roles = "admin")]
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

    [HttpGet("{id:guid}/books")]
    public async Task<IActionResult> GetBooksWithAuthorId([FromRoute] Guid id)
    {
        var booksDto = await _service.BookService.GetBooksByAuthorIdAsync(id, trackChanges: false);
        return Ok(booksDto);
    }
}