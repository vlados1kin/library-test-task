using AutoMapper;
using Library.Contracts;
using Library.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Library.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<IImageService> _imageService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper, IOptions<ImageSettings> options)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repository, mapper));
        _bookService = new Lazy<IBookService>(() => new BookService(repository, mapper));
        _imageService = new Lazy<IImageService>(() => new ImageService(options));
    }

    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
    public IImageService ImageService => _imageService.Value;
}