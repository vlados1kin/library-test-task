using AutoMapper;
using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Library.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<IGenreService> _genreService;
    private readonly Lazy<IImageService> _imageService;
    private readonly Lazy<IUserService> _authenticationService;
    private readonly Lazy<IIssueService> _issueService;

    public ServiceManager(
        IRepositoryManager repository, 
        IMapper mapper, 
        IOptions<ImageSettings> options,
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repository, mapper));
        _bookService = new Lazy<IBookService>(() => new BookService(repository, mapper));
        _genreService = new Lazy<IGenreService>(() => new GenreService(repository, mapper));
        _issueService = new Lazy<IIssueService>(() => new IssueService(repository, mapper));
        _imageService = new Lazy<IImageService>(() => new ImageService(options));
        _authenticationService = new Lazy<IUserService>(() => new UserService(mapper, userManager, configuration));
    }

    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
    public IGenreService GenreService => _genreService.Value;
    public IIssueService IssueService => _issueService.Value;
    public IImageService ImageService => _imageService.Value;
    public IUserService UserService => _authenticationService.Value;
}