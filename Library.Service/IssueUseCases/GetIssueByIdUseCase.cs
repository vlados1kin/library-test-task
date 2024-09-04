using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Shared.DTO;

namespace Library.Service.IssueUseCases;

public class GetIssueByIdUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public GetIssueByIdUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IssueDto> ExecuteAsync(Guid id, bool trackChanges)
    {
        var issue = await _repository.Issue.GetIssueByIdAsync(id, trackChanges);
        if (issue is null)
            throw new IssueNotFoundException(id);
        var issueDto = _mapper.Map<IssueDto>(issue);
        return issueDto;
    }
}