using Library.Contracts;
using Library.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/issues")]
public class IssueController : ControllerBase
{
    private readonly IServiceManager _service;

    public IssueController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetIssues()
    {
        var issueDtos = await _service.IssueService.GetIssuesAsync(trackChanges: false);
        return Ok(issueDtos);
    }

    [HttpGet("{id:guid}", Name = "GetIssueById")]
    public async Task<IActionResult> GetIssueById([FromRoute] Guid id)
    {
        var issue = await _service.IssueService.GetIssueByIdAsync(id, trackChanges: false);
        return Ok(issue);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] IssueForCreationDto issueForCreationDto)
    {
        var issueDto = await _service.IssueService.CreateIssueAsync(issueForCreationDto);
        return CreatedAtRoute("GetIssueById", new { Id = issueDto.Id }, issueDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIssue([FromRoute] Guid id, [FromBody] IssueForUpdateDto issueForUpdateDto)
    {
        await _service.IssueService.UpdateIssueAsync(id, issueForUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIssue([FromRoute] Guid id)
    {
        await _service.IssueService.DeleteIssueAsync(id, trackChanges: true);
        return NoContent();
    }
}