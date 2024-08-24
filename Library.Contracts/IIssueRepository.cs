using Library.Domain.Models;

namespace Library.Contracts;

public interface IIssueRepository
{
    Task<IEnumerable<Issue>> GetIssuesAsync(bool trackChanges);
    Task<IEnumerable<Issue>> GetIssuesByUserIdAsync(Guid id, bool trackChanges);
    Task<Issue> GetIssueByIdAsync(Guid id, bool trackChanges);
    void CreateIssue(Issue issue);
    void DeleteIssue(Issue issue);
}