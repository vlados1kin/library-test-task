using AutoMapper;
using Library.Contracts;

namespace Library.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repository, mapper));
        _bookService = new Lazy<IBookService>(() => new BookService(repository, mapper));
    }

    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
}