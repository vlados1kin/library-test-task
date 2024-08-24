using Library.Contracts;

namespace Library.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IAuthorRepository> _authorRepository;
    private readonly Lazy<IBookRepository> _bookRepository;
    private readonly Lazy<IGenreRepository> _genreRepository;
    private readonly Lazy<IIssueRepository> _issueRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(repositoryContext));
        _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(repositoryContext));
        _genreRepository = new Lazy<IGenreRepository>(() => new GenreRepository(repositoryContext));
        _issueRepository = new Lazy<IIssueRepository>(() => new IssueRepository(repositoryContext));
    }

    public IAuthorRepository Author => _authorRepository.Value;
    public IBookRepository Book => _bookRepository.Value;
    public IGenreRepository Genre => _genreRepository.Value;
    public IIssueRepository Issue => _issueRepository.Value;
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}