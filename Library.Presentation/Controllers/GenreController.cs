using Library.Service.GenreUseCases;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly GetGenresUseCase _getGenresUseCase;
    private readonly GetGenreByIdUseCase _getGenreByIdUseCase;
    private readonly CreateGenreUseCase _createGenreUseCase;
    private readonly UpdateGenreUseCase _updateGenreUseCase;
    private readonly DeleteGenreUseCase _deleteGenreUseCase;
    
    public GenreController(
        GetGenresUseCase getGenresUseCase,
        GetGenreByIdUseCase getGenreByIdUseCase,
        CreateGenreUseCase createGenreUseCase,
        UpdateGenreUseCase updateGenreUseCase,
        DeleteGenreUseCase deleteGenreUseCase)
    {
        _getGenresUseCase = getGenresUseCase;
        _getGenreByIdUseCase = getGenreByIdUseCase;
        _createGenreUseCase = createGenreUseCase;
        _updateGenreUseCase = updateGenreUseCase;
        _deleteGenreUseCase = deleteGenreUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genresDto = await _getGenresUseCase.ExecuteAsync(trackChanges: false);
        return Ok(genresDto);
    }
    
    [HttpGet("{id:guid}", Name = "GetGenreById")]
    public async Task<IActionResult> GetGenreById(Guid id)
    {
        var genreDto = await _getGenreByIdUseCase.ExecuteAsync(id, trackChanges: false);
        return Ok(genreDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] GenreForCreationDto genreForCreationDto)
    {
        var genreDto = await _createGenreUseCase.ExecuteAsync(genreForCreationDto);
        return CreatedAtRoute("GetGenreById", new { Id = genreDto.Id }, genreDto);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] GenreForUpdateDto genreForUpdateDto)
    {
        await _updateGenreUseCase.ExecuteAsync(id, genreForUpdateDto, trackChanges: true);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        await _deleteGenreUseCase.ExecuteAsync(id, trackChanges: true);
        return NoContent();
    }
}