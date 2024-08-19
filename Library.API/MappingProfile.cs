using AutoMapper;
using Library.Domain.Models;
using Library.Service;
using Library.Shared.DTO;

namespace Library.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();

        CreateMap<AuthorForCreationDto, Author>();

        CreateMap<AuthorForUpdateDto, Author>();
    }
}