using Library.Contracts;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IServiceManager _service;

    public BookController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var bookDto = await _service.BookService.GetBooksAsync(trackChanges: false);
        return Ok(bookDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookById([FromRoute] Guid id)
    {
        var bookDto = await _service.BookService.GetBookByIdAsync(id, trackChanges: false);
        return Ok(bookDto);
    }

    [HttpGet("{isbn}", Name = "GetBookById")]
    public async Task<IActionResult> GetBookByIsbn([FromRoute] string isbn)
    {
        var bookDto = await _service.BookService.GetBookByIsbnAsync(isbn, trackChanges: false);
        return Ok(bookDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto bookForCreationDto)
    {
        var bookDto = await _service.BookService.CreateBookAsync(bookForCreationDto);
        return CreatedAtRoute("GetBookById", new { id = bookDto.Id }, bookDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBook([FromRoute] Guid id, [FromBody] BookForUpdateDto bookForUpdateDto)
    {
        await _service.BookService.UpdateBookAsync(id, bookForUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
    {
        await _service.BookService.DeleteBookAsync(id, trackChanges: true);
        return NoContent();
    }

    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> IssueBookToUser([FromRoute] Guid id, [FromBody] BookForIssueDto bookForIssueDto)
    // {
    //     await _service.BookService.IssueBookToUser(id, bookForIssueDto, trackChanges: true);
    //     return NoContent();
    // }
}