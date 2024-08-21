using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(BookParameters bookParameters, bool trackChanges)
        => await FindAll(trackChanges)
            .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
            .Take(bookParameters.PageSize)
            .Include(b => b.Genre)
            .ToListAsync();

    public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId, bool trackChanges)
        => await FindByCondition(b => b.AuthorId.Equals(authorId), trackChanges).Include(b => b.Genre).ToListAsync();

    public async Task<Book> GetBookByIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(b => b.Id.Equals(id), trackChanges).Include(b => b.Genre).SingleOrDefaultAsync();

    public async Task<Book> GetBookByIsbnAsync(string isbn, bool trackChanges)
        => await FindByCondition(b => b.ISBN.Equals(isbn), trackChanges).Include(b => b.Genre).SingleOrDefaultAsync();

    public void CreateBook(Book book) => Create(book);

    public void DeleteBook(Book book) => Delete(book);
}