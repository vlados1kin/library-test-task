﻿using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Contracts;

public interface IBookService
{
    Task<(IEnumerable<BookDto> bookDtos, MetaData metaData)> GetBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<BookDto> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<BookDto> GetBookByIsbnAsync(string isbn, bool trackChanges);
    Task<BookDto> CreateBookAsync(BookForCreationDto bookForCreationDto);
    Task UpdateBookAsync(Guid id, BookForUpdateDto bookForUpdateDto, bool trackChanges);
    Task DeleteBookAsync(Guid id, bool trackChanges);
    Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(Guid id, bool trackChanges);
}