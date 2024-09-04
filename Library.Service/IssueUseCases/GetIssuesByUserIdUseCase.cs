using AutoMapper;
using Library.Contracts;
using Library.Shared.DTO;

namespace Library.Service.IssueUseCases;

public class GetIssuesByUserIdUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public GetIssuesByUserIdUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<IssueDto>> ExecuteAsync(Guid id, bool trackChanges)
    {
        var issues = await _repository.Issue.GetIssuesByUserIdAsync(id, trackChanges);
        var issuesDto = _mapper.Map<IEnumerable<IssueDto>>(issues);
        return issuesDto;
    }
}