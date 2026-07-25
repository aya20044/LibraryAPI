using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Mappings
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Books, opt => opt.Ignore());
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>()
             .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}
