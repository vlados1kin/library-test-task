﻿using AutoMapper;
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
    private readonly Lazy<IAuthenticationService> _authenticationService;

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
        _imageService = new Lazy<IImageService>(() => new ImageService(options));
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration));
    }

    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
    public IGenreService GenreService => _genreService.Value;
    public IImageService ImageService => _imageService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}