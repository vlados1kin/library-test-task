using AutoMapper;
using Library.Contracts;
using Library.Shared.DTO;

namespace Library.Service.IssueUseCases;

public class GetIssuesUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public GetIssuesUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<IssueDto>> ExecuteAsync(bool trackChanges)
    {
        var issues = await _repository.Issue.GetIssuesAsync(trackChanges);
        var issuesDto = _mapper.Map<IEnumerable<IssueDto>>(issues);
        return issuesDto;
    }
}