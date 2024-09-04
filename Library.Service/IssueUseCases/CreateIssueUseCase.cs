using AutoMapper;
using Library.Contracts;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.Service.IssueUseCases;

public class CreateIssueUseCase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public CreateIssueUseCase(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IssueDto> ExecuteAsync(IssueForCreationDto issueDtoForCreationDto)
    {
        var issue = _mapper.Map<Issue>(issueDtoForCreationDto);
        _repository.Issue.Create(issue);
        await _repository.SaveAsync();
        var issueDto = _mapper.Map<IssueDto>(issue);
        return issueDto;
    }
}