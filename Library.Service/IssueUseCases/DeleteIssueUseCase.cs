using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;

namespace Library.Service.IssueUseCases;

public class DeleteIssueUseCase
{
    private readonly IRepositoryManager _repository;
    
    public DeleteIssueUseCase(IRepositoryManager repository) => _repository = repository; 
    
    public async Task ExecuteAsync(Guid id, bool trackChanges)
    {
        var issue = await _repository.Issue.GetIssueByIdAsync(id, trackChanges);
        if (issue is null)
            throw new IssueNotFoundException(id);
        _repository.Issue.Delete(issue);
        await _repository.SaveAsync();
    }
}