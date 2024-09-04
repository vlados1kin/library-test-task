using Library.Domain.Models;

namespace Library.Contracts;

public interface IIssueRepository : IRepositoryBase<Issue>
{
    Task<IEnumerable<Issue>> GetIssuesAsync(bool trackChanges);
    Task<IEnumerable<Issue>> GetIssuesByUserIdAsync(Guid id, bool trackChanges);
    Task<Issue> GetIssueByIdAsync(Guid id, bool trackChanges);
}