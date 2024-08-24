using AutoMapper;
using Library.Domain.Models;
using Library.Shared.DTO;

namespace Library.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();

        CreateMap<AuthorForCreationDto, Author>();

        CreateMap<AuthorForUpdateDto, Author>();

        CreateMap<Book, BookDto>();

        CreateMap<BookForCreationDto, Book>();

        CreateMap<BookForUpdateDto, Book>();

        CreateMap<BookForIssueDto, Book>();

        CreateMap<UserForRegistrationDto, User>();

        CreateMap<Genre, GenreDto>();

        CreateMap<GenreForCreationDto, Genre>();

        CreateMap<GenreForUpdateDto, Genre>();

        CreateMap<Issue, IssueDto>();

        CreateMap<IssueForCreationDto, Issue>();

        CreateMap<IssueForUpdateDto, Issue>();

        CreateMap<User, UserDto>();
    }
}