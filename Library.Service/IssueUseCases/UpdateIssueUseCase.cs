using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.IssueUseCases;

public class UpdateIssueUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public UpdateIssueUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task ExecuteAsync(Guid id, IssueForUpdateDto issueForUpdateDto, bool trackChanges)
    {
        var issue = await _repository.Issue.GetIssueByIdAsync(id, trackChanges);
        if (issue is null)
            throw new IssueNotFoundException(id);
        _mapper.Map(issueForUpdateDto, issue);
        await _repository.SaveAsync();
    }
}