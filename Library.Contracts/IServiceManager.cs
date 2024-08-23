﻿namespace Library.Contracts;

public interface IServiceManager
{
    IAuthorService AuthorService { get; }
    IBookService BookService { get; }
    IImageService ImageService { get; }
    IAuthenticationService AuthenticationService { get; }
}