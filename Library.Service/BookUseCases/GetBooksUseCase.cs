﻿using AutoMapper;
using Library.Contracts;
using Library.Domain.Settings;
using Library.Shared.DTO;
using Library.Shared.RequestFeatures;

namespace Library.Service.BookUseCases;

public class GetBooksUseCase
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public GetBooksUseCase(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<(IEnumerable<BookDto> bookDtos, MetaData metaData)> ExecuteAsync(BookParameters bookParameters, bool trackChanges)
    {
        var booksWithMetaData = await _repository.GetBooksAsync(bookParameters, trackChanges);
        var bookDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
        return (bookDtos: bookDto, metaData: booksWithMetaData.MetaData);
    }
}