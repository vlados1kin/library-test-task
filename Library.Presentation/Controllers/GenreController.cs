using Library.Contracts;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly IServiceManager _service;

    public GenreController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genresDto = await _service.GenreService.GetGenresAsync(trackChanges: false);
        return Ok(genresDto);
    }
    
    [HttpGet("{id:guid}", Name = "GetGenreById")]
    public async Task<IActionResult> GetGenreById(Guid id)
    {
        var genreDto = await _service.GenreService.GetGenreByIdAsync(id, trackChanges: false);
        return Ok(genreDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] GenreForCreationDto genreForCreationDto)
    {
        var genreDto = await _service.GenreService.CreateGenreAsync(genreForCreationDto);
        return CreatedAtRoute("GetGenreById", new { Id = genreDto.Id }, genreDto);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] GenreForUpdateDto genreForUpdateDto)
    {
        await _service.GenreService.UpdateGenreAsync(id, genreForUpdateDto, trackChanges: true);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        await _service.GenreService.DeleteGenreAsync(id, trackChanges: true);
        return NoContent();
    }
}