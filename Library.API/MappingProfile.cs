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

        CreateMap<Book, BookDto>().ForMember(bd => bd.Genre, opt => opt.MapFrom(src => src.Genre.Name));

        CreateMap<BookForCreationDto, Book>();

        CreateMap<BookForUpdateDto, Book>();

        CreateMap<BookForIssueDto, Book>();

        CreateMap<UserForRegistrationDto, User>();
    }
}