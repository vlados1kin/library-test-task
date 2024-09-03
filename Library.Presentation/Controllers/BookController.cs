using System.Text.Json;
using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Service.AuthorUseCases;
using Library.Service.BookUseCases;
using Library.Service.ImageUseCases;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly GetBooksUseCase _getBooksUseCase;
    private readonly GetBookByIdUseCase _getBookByIdUseCase;
    private readonly GetBookByIsbnUseCase _getBookByIsbnUseCase;
    private readonly CreateBookUseCase _createBookUseCase;
    private readonly UpdateBookUseCase _updateBookUseCase;
    private readonly DeleteBookUseCase _deleteBookUseCase;
    private readonly DownloadImageUseCase _downloadImageUseCase;
    private readonly UploadImageUseCase _uploadImageUseCase;
    
    public BookController(
        GetBooksUseCase getBooksUseCase,
        GetBookByIdUseCase getBookByIdUseCase,
        GetBookByIsbnUseCase getBookByIsbnUseCase,
        CreateBookUseCase createBookUseCase,
        UpdateBookUseCase updateBookUseCase,
        DeleteBookUseCase deleteBookUseCase,
        DownloadImageUseCase downloadImageUseCase,
        UploadImageUseCase uploadImageUseCase)
    {
        _getBooksUseCase = getBooksUseCase;
        _getBookByIdUseCase = getBookByIdUseCase;
        _getBookByIsbnUseCase = getBookByIsbnUseCase;
        _createBookUseCase = createBookUseCase;
        _updateBookUseCase = updateBookUseCase;
        _deleteBookUseCase = deleteBookUseCase;
        _downloadImageUseCase = downloadImageUseCase;
        _uploadImageUseCase = uploadImageUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] BookParameters bookParameters)
    {
        var bookDtoWithMetaData = await _getBooksUseCase.ExecuteAsync(bookParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(bookDtoWithMetaData.metaData));
        return Ok(bookDtoWithMetaData.bookDtos);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    public async Task<IActionResult> GetBookById([FromRoute] Guid id)
    {
        var bookDto = await _getBookByIdUseCase.ExecuteAsync(id, trackChanges: false);
        return Ok(bookDto);
    }

    [HttpGet("{isbn}")]
    public async Task<IActionResult> GetBookByIsbn([FromRoute] string isbn)
    {
        var bookDto = await _getBookByIsbnUseCase.ExecuteAsync(isbn, trackChanges: false);
        return Ok(bookDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto bookForCreationDto)
    {
        var bookDto = await _createBookUseCase.ExecuteAsync(bookForCreationDto);
        return CreatedAtRoute("GetBookById", new { id = bookDto.Id }, bookDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBook([FromRoute] Guid id, [FromBody] BookForUpdateDto bookForUpdateDto)
    {
        await _updateBookUseCase.ExecuteAsync(id, bookForUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
    {
        await _deleteBookUseCase.ExecuteAsync(id, trackChanges: true);
        return NoContent();
    }

    [HttpGet("{id}/image", Name = "DownloadImage")]
    public async Task<IActionResult> DownloadImage([FromRoute] Guid id)
    {
        var result = await _downloadImageUseCase.ExecuteAsync(id);
        return File(result.fileBytes, result.contentType, result.fileName);
    }
    
    [HttpPost("{id:guid}/image")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid id, [FromForm] UploadImage uploadImage)
    {
        await _uploadImageUseCase.ExecuteAsync(id, uploadImage);
        return CreatedAtRoute("DownloadImage", new { Id = id }, null);
    }
}