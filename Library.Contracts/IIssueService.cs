using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Contracts;

public interface IIssueService
{
    Task<IEnumerable<IssueDto>> GetIssuesAsync(bool trackChanges);
    Task<IEnumerable<IssueDto>> GetIssuesByUserIdAsync(Guid id, bool trackChanges);
    Task<IssueDto> GetIssueByIdAsync(Guid id, bool trackChanges);
    Task<IssueDto> CreateIssueAsync(IssueForCreationDto issueDtoForCreationDto);
    Task UpdateIssueAsync(Guid id, IssueForUpdateDto issueForUpdateDto, bool trackChanges);
    Task DeleteIssueAsync(Guid id, bool trackChanges);
}