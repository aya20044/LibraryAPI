using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService : GenericService<Book,BookDto>,IBookService
    {
        public BookService(AppDbContext Context,IMapper Mapper)
            : base(Context,Mapper)
        {

        }
        public override async Task<BookDto?> Add(BookDto dto)
        {
            var authorExists = await Context.Authors.AnyAsync(
                author=>author.Id == dto.AuthorId
                );
            if (!authorExists)
            {
                return default;
            }
            var categoryExists = await Context.Categories.AnyAsync(
                category=>category.Id == dto.CategoryId
                );
            if (!categoryExists)
            {
                return default;
            }
            var book = Mapper.Map<Book>(dto);
            await Entities.AddAsync(book);
            await Context.SaveChangesAsync();
            return Mapper.Map<BookDto>(book);
        }
        public override async Task<BookDto?> Edit(int id,BookDto dto)
        {
            var book = await Entities
                .FirstOrDefaultAsync(book => book.Id == id);
            if (book==null)
                return default;
            var authorExists = await Context.Authors.AnyAsync(
                author => author.Id == dto.AuthorId
                );
            if (!authorExists)
                return default;
            var categoryExists = await Context.Categories.AnyAsync(
                category => category.Id == dto.CategoryId
                );
            if (!categoryExists)
                return default;
            Mapper.Map(dto, book);
            await Context.SaveChangesAsync();

            return Mapper.Map<BookDto>(book);

        }
        public async Task<IEnumerable<BookDto>> Search(string searchTerm)
        {
            var books = await Entities.AsNoTracking()
                .Where(book => book.Title.Contains(searchTerm))
                .ToListAsync();
            return Mapper.Map<IEnumerable<BookDto>>(books);

        }
        public async Task<IEnumerable<BookDto>> SortByTitle(string? sortDirection)
        {
            var query = Entities.AsNoTracking();
            if (string.Equals(sortDirection,"desc",StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderByDescending(book => book.Title);
            }
            else
            {
                query = query.OrderBy(book => book.Title);
            }
            var books = await query.ToListAsync();
            return Mapper.Map<IEnumerable<BookDto>>(books);
        }
        public async Task<IEnumerable<BookDto>> GetPaged(int pageNumber,int pageSize)
        {
            var books = await Entities.AsNoTracking()
                .OrderBy(book => book.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}
