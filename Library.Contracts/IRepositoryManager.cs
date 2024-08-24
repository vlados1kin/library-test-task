namespace Library.Contracts;

public interface IRepositoryManager
{
    IAuthorRepository Author { get; }
    IBookRepository Book { get; }
    IGenreRepository Genre { get; }
    IIssueRepository Issue { get; }
    Task SaveAsync();
}