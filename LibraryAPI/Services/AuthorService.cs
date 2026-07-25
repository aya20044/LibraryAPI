using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class AuthorService : GenericService<Author,AuthorDto>,IAuthorService
    {
        public AuthorService(AppDbContext context,IMapper mapper)
            : base(context,mapper)
        {

        }
        public override async Task<IEnumerable<AuthorDto>> GetAll()
        {
            var authors = await Entities.AsNoTracking()
                          .Include(author => author.Books)
                          .ToListAsync();
            return Mapper.Map<IEnumerable<AuthorDto>>(authors);
        }
        public override async Task<AuthorDto?> GetById(int id)
        {
            var author = await Entities.AsNoTracking()
                         .Include(author => author.Books)
                         .FirstOrDefaultAsync(author => author.Id == id);
            if (author == null)
                return null;
            return Mapper.Map<AuthorDto>(author);
        }
        public override async Task<bool> Delete(int id)
        {
            var hasBooks =  await Context.Books
                         .AnyAsync(book => book.AuthorId == id);
            if (hasBooks)
                return false;

            var author = await Entities.FindAsync(id);

            if (author == null)
                return false;

            Entities.Remove(author);
            await Context.SaveChangesAsync();
            return true;

        }

    }
}
