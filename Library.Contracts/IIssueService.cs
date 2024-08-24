using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Contracts;

public interface IIssueService
{
    Task<IEnumerable<IssueDto>> GetIssuesAsync(bool trackChanges);
    Task<IssueDto> GetIssueByIdAsync(Guid id, bool trackChanges);
    Task CreateIssueAsync(IssueForCreationDto issueDtoForCreationDto);
    Task UpdateIssueAsync(Guid id, IssueForUpdateDto issueForUpdateDto, bool trackChanges);
    Task DeleteIssue(Guid id, bool trackChanges);
}