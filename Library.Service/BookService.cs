using AutoMapper;
using Library.Contracts;
using Library.Domain.Exceptions;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Service;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public BookService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<BookDto> bookDtos, MetaData metaData)> GetBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
        var booksWithMetaData = await _repository.Book.GetBooksAsync(bookParameters, trackChanges);
        var bookDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
        return (bookDtos: bookDto, metaData: booksWithMetaData.MetaData);
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }

    public async Task<BookDto> GetBookByIsbnAsync(string isbn, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIsbnAsync(isbn, trackChanges);
        if (book is null)
            throw new BookWithIsbnNotFoundException(isbn);
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }

    public async Task<BookDto> CreateBookAsync(BookForCreationDto bookForCreationDto)
    {
        var book = _mapper.Map<Book>(bookForCreationDto);
        _repository.Book.CreateBook(book);
        await _repository.SaveAsync();
        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }

    public async Task UpdateBookAsync(Guid id, BookForUpdateDto bookForUpdateDto, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        _mapper.Map(bookForUpdateDto, book);
        await _repository.SaveAsync();
    }

    public async Task DeleteBookAsync(Guid id, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookWithIdNotFoundException(id);
        _repository.Book.DeleteBook(book);
        await _repository.SaveAsync();
    }
    
    public async Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(Guid id, bool trackChanges)
    {
        var books = await _repository.Book.GetBooksByAuthorIdAsync(id, trackChanges);
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return booksDto;
    }
}