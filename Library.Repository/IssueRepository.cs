using Library.Contracts;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
{
    public IssueRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Issue>> GetIssuesAsync(bool trackChanges)
        => await FindAll(trackChanges).ToListAsync();
    
    public async Task<IEnumerable<Issue>> GetIssuesByUserIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(i => i.UserId.Equals(id), trackChanges).Include(i => i.Book).ToListAsync();

    public async Task<Issue> GetIssueByIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(i => i.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    
    public void CreateIssue(Issue issue) => Create(issue);

    public void DeleteIssue(Issue issue) => Delete(issue);
}