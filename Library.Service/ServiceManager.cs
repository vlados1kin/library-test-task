using AutoMapper;
using Library.Contracts;

namespace Library.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorService> _authorService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repository, mapper));
    }

    public IAuthorService AuthorService => _authorService.Value;
}