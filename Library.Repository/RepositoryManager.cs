using Library.Contracts;

namespace Library.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IAuthorRepository> _authorRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(repositoryContext));
    }

    public IAuthorRepository Author => _authorRepository.Value;
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}