using Library.Service.IssueUseCases;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/issues")]
public class IssueController : ControllerBase
{
    private readonly GetIssuesUseCase _getIssuesUseCase;
    private readonly GetIssueByIdUseCase _getIssueByIdUseCase;
    private readonly CreateIssueUseCase _createIssueUseCase;
    private readonly UpdateIssueUseCase _updateIssueUseCase;
    private readonly DeleteIssueUseCase _deleteIssueUseCase;

    public IssueController(
        GetIssuesUseCase getIssuesUseCase,
        GetIssueByIdUseCase getIssueByIdUseCase,
        CreateIssueUseCase createIssueUseCase,
        UpdateIssueUseCase updateIssueUseCase,
        DeleteIssueUseCase deleteIssueUseCase)
    {
        _getIssuesUseCase = getIssuesUseCase;
        _getIssueByIdUseCase = getIssueByIdUseCase;
        _createIssueUseCase = createIssueUseCase;
        _updateIssueUseCase = updateIssueUseCase;
        _deleteIssueUseCase = deleteIssueUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetIssues()
    {
        var issueDtos = await _getIssuesUseCase.ExecuteAsync(trackChanges: false);
        return Ok(issueDtos);
    }

    [HttpGet("{id:guid}", Name = "GetIssueById")]
    public async Task<IActionResult> GetIssueById([FromRoute] Guid id)
    {
        var issue = await _getIssueByIdUseCase.ExecuteAsync(id, trackChanges: false);
        return Ok(issue);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] IssueForCreationDto issueForCreationDto)
    {
        var issueDto = await _createIssueUseCase.ExecuteAsync(issueForCreationDto);
        return CreatedAtRoute("GetIssueById", new { Id = issueDto.Id }, issueDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIssue([FromRoute] Guid id, [FromBody] IssueForUpdateDto issueForUpdateDto)
    {
        await _updateIssueUseCase.ExecuteAsync(id, issueForUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIssue([FromRoute] Guid id)
    {
        await _deleteIssueUseCase.ExecuteAsync(id, trackChanges: true);
        return NoContent();
    }
}