using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IBookService : IGenericService<Book,BookDto>
    {
        Task<IEnumerable<BookDto>> Search(string searchTerm);
        Task<IEnumerable<BookDto>> SortByTitle(string sortDirection);
        Task<IEnumerable<BookDto>> GetPaged(int pageNumber, int pageSize);
    }
}
