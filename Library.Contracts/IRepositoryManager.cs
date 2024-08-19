namespace Library.Contracts;

public interface IRepositoryManager
{
    IAuthorRepository Author { get; }
    Task SaveAsync();
}