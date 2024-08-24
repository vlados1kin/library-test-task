namespace Library.Contracts;

public interface IServiceManager
{
    IAuthorService AuthorService { get; }
    IBookService BookService { get; }
    IGenreService GenreService { get; }
    IImageService ImageService { get; }
    IUserService UserService { get; }
}