using AutoMapper;
using LibManager.Models.DTOs;
using LibManager.Models.Entities;

namespace LibManager.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<Book, BookDto>();
    }
}