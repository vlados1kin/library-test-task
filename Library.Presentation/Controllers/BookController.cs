using System.Text.Json;
using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
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
    public async Task<IActionResult> GetBooks([FromQuery] BookParameters bookParameters)
    {
        var bookDtoWithMetaData = await _service.BookService.GetBooksAsync(bookParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(bookDtoWithMetaData.metaData));
        return Ok(bookDtoWithMetaData.bookDtos);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    public async Task<IActionResult> GetBookById([FromRoute] Guid id)
    {
        var bookDto = await _service.BookService.GetBookByIdAsync(id, trackChanges: false);
        return Ok(bookDto);
    }

    [HttpGet("{isbn}")]
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

    [HttpPut("issue/{id:guid}")]
    public async Task<IActionResult> IssueBook([FromRoute] Guid id, [FromBody] BookForIssueDto bookForIssueDto)
    {
        await _service.BookService.IssueBookAsync(id, bookForIssueDto, trackChanges: true);
        return NoContent();
    }

    [HttpGet("download-image/{fileNameWithExtension}", Name = "DownloadImage")]
    public async Task<IActionResult> DownloadImage([FromRoute] string fileNameWithExtension)
    {
        var result = await _service.ImageService.DownloadImageAsync(fileNameWithExtension);
        return File(result.fileBytes, result.contentType, result.fileName);
    }
    
    [HttpPost("upload-image/{id:guid}")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid id, [FromForm] UploadImage uploadImage)
    {
        var fileName = await _service.ImageService.UploadImageAsync(id, uploadImage);
        return CreatedAtRoute("DownloadImage", new { fileNameWithExtension = fileName }, fileName);
    }
}