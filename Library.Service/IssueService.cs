using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service;

public class IssueService : IIssueService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public IssueService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<IssueDto>> GetIssuesAsync(bool trackChanges)
    {
        var issues = await _repository.Issue.GetIssuesAsync(trackChanges);
        var issuesDto = _mapper.Map<IEnumerable<IssueDto>>(issues);
        return issuesDto;
    }

    public async Task<IssueDto> GetIssueByIdAsync(Guid id, bool trackChanges)
    {
        var issue = await _repository.Issue.GetIssueByIdAsync(id, trackChanges);
        if (issue is null)
            throw new IssueNotFoundException(id);
        var issueDto = _mapper.Map<IssueDto>(issue);
        return issueDto;
    }

    public async Task CreateIssueAsync(IssueForCreationDto issueDtoForCreationDto)
    {
        var issue = _mapper.Map<Issue>(issueDtoForCreationDto);
        _repository.Issue.CreateIssue(issue);
        await _repository.SaveAsync();
    }

    public async Task UpdateIssueAsync(Guid id, IssueForUpdateDto issueForUpdateDto, bool trackChanges)
    {
        var issue = await _repository.Issue.GetIssueByIdAsync(id, trackChanges);
        if (issue is null)
            throw new IssueNotFoundException(id);
        _mapper.Map(issueForUpdateDto, issue);
        await _repository.SaveAsync();
    }

    public async Task DeleteIssue(Guid id, bool trackChanges)
    {
        var issue = await _repository.Issue.GetIssueByIdAsync(id, trackChanges);
        if (issue is null)
            throw new IssueNotFoundException(id);
        _repository.Issue.DeleteIssue(issue);
        await _repository.SaveAsync();
    }
}