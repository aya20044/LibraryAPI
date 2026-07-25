using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class CategoryService : GenericService<Category,CategoryDto>, ICategoryService
    {
        public CategoryService(AppDbContext Context,IMapper Mapper)
            : base(Context,Mapper)
        {

        }
        public override async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await Entities.AsNoTracking()
                .Include(category => category.Books).ToListAsync();
            return Mapper.Map<List<CategoryDto>>(categories);
        }
        public override async Task<CategoryDto?> GetById(int id)
        {
            var category = await Entities.AsNoTracking().
                Include(category => category.Books)
                .FirstOrDefaultAsync(category => category.Id == id);
            if (category == null)
                return default;
            return Mapper.Map<CategoryDto>(category);
        }
        public override async Task<bool> Delete(int id)
        {
            var hasBooks = await Context.Books
                         .AnyAsync(book => book.AuthorId == id);
            if (hasBooks)
                return false;

            var category = await Entities.FindAsync(id);

            if (category== null)
                return false;

            Entities.Remove(category);
            await Context.SaveChangesAsync();
            return true;

        }
    }
}
