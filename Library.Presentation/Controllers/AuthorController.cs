using Library.Contracts;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Library.Service.AuthorUseCases;
using Microsoft.AspNetCore.Authorization;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly GetAuthorsUseCase _getAuthorsUseCase;
    private readonly GetAuthorByIdUseCase _getAuthorByIdUseCase;
    private readonly CreateAuthorUseCase _createAuthorUseCase;
    private readonly UpdateAuthorUseCase _updateAuthorUseCase;
    private readonly DeleteAuthorUseCase _deleteAuthorUseCase;
    
    public AuthorController(
        GetAuthorsUseCase getAuthorsUseCase,
        GetAuthorByIdUseCase getAuthorByIdUseCase,
        CreateAuthorUseCase createAuthorUseCase,
        UpdateAuthorUseCase updateAuthorUseCase,
        DeleteAuthorUseCase deleteAuthorUseCase)
    {
        _getAuthorsUseCase = getAuthorsUseCase;
        _getAuthorByIdUseCase = getAuthorByIdUseCase;
        _createAuthorUseCase = createAuthorUseCase;
        _updateAuthorUseCase = updateAuthorUseCase;
        _deleteAuthorUseCase = deleteAuthorUseCase;
    }

    [HttpGet(Name = "GetAuthors")]
    public async Task<IActionResult> GetAuthors([FromQuery] AuthorParameters authorParameters)
    {
        var authorDtosWithMetaData = await _getAuthorsUseCase.ExecuteAsync(authorParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(authorDtosWithMetaData.metaData));
        return Ok(authorDtosWithMetaData.authorDtos);
    }

    [HttpGet("{id:guid}", Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthorById([FromRoute] Guid id)
    {
        var authorDto = await _getAuthorByIdUseCase.ExecuteAsync(id, trackChanges: false);
        return Ok(authorDto);
    }
    
    [HttpGet("{id:guid}/books")]
    public async Task<IActionResult> GetBooksWithAuthorId([FromRoute] Guid id)
    {
        //var booksDto = await _service.BookService.GetBooksByAuthorIdAsync(id, trackChanges: false);
        return Ok(/*booksDto*/);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorForCreationDto authorForCreationDto)
    {
        var authorDto = await _createAuthorUseCase.ExecuteAsync(authorForCreationDto);
        return CreatedAtRoute("GetAuthorById", new { id = authorDto.Id }, authorDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAuthor([FromRoute] Guid id, [FromBody] AuthorForUpdateDto authorForUpdateDto)
    {
        await _updateAuthorUseCase.ExecuteAsync(id, authorForUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor([FromRoute] Guid id)
    {
        await _deleteAuthorUseCase.ExecuteAsync(id, trackChanges: true);
        return NoContent();
    }
}