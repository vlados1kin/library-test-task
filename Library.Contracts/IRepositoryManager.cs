namespace Library.Contracts;

public interface IRepositoryManager
{
    IAuthorRepository Author { get; }
    IBookRepository Book { get; }
    IGenreRepository Genre { get; }
    Task SaveAsync();
}