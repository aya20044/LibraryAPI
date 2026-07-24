using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IAuthorService : IGenericService<Author,AuthorDto>
    {
        Task<bool> HasBooks(int authorId);
    }
}
